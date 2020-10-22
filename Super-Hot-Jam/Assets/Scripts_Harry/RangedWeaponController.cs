using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    [Header("Setup")]
    public RangedWeaponSO weaponSettings; // Get the weapons attributes and settings
    public GameObject projectile;
    public Transform firePoint;

    [SerializeField] private float currentAmmo = 0;
    [SerializeField] private bool weaponActive = true;

    private float shotTimer; // Time until next shot
    private float throwTimer;
    private bool hasBeenThrown;
    
    Animator anim;
    Rigidbody2D rb;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        currentAmmo = weaponSettings.ammoCount;
    }

    private void Update()
    {
        if (weaponActive)
        {
            FireWeapon();
            ThrowWeapon();
        }
    }

    void FireWeapon()
    {
        shotTimer -= Time.deltaTime; // Reduce the time until next shot is available

        if (Input.GetButtonDown("Fire1") && currentAmmo != 0)
        {
            if (shotTimer <= 0) // If next shot is ready
            {
                if (!weaponSettings.hasSpread) // If the weapon doesn't have a spread (i.e not a shotgun)
                {
                    anim.SetTrigger("Firing");

                    Instantiate(projectile, firePoint.position, firePoint.rotation);

                    shotTimer = weaponSettings.fireRate; // Resets shot counter dependant on the weapons fire rate
                    currentAmmo--;
                }

                if (weaponSettings.hasSpread) // If the weapon has spread (i.e is a shotgun)
                {
                    anim.SetTrigger("Firing");

                    float angleStep = weaponSettings.spreadAngle / weaponSettings.projectileCount;
                    float aimingAngle = firePoint.rotation.eulerAngles.z;
                    float centeringOffset = (weaponSettings.spreadAngle / 2) - (angleStep / 2); // Offsets every projectile so the spread occurs 

                    for (int i = 0; i < weaponSettings.projectileCount; i++)
                    {
                        float currentBulletAngle = angleStep * i;

                        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulletAngle - centeringOffset));
                        GameObject bullet = Instantiate(projectile, firePoint.position, rotation);
                    }

                    shotTimer = weaponSettings.fireRate; // Resets shot counter dependant on the weapons fire rate
                    currentAmmo--;
                }
            }
        }
    }

    void ThrowWeapon()
    {
        if(Input.GetButton("Fire1"))
        {
            throwTimer += Time.deltaTime; // Begin the timer for throwing a gun
        }

        if (throwTimer >= weaponSettings.throwTime)  // If the timer is complete
        {
            Debug.Log("READY!");

            if (Input.GetButtonUp("Fire1")) // If the player releases the A button
            {
                rb.AddForce(new Vector2(0, -weaponSettings.throwForce * Time.deltaTime), ForceMode2D.Impulse); // Apply a force to the weapon
                rb.AddTorque(weaponSettings.throwTorque * Time.deltaTime, ForceMode2D.Impulse);

                transform.SetParent(null); // Removes the parent from the weapon

                throwTimer = 0;
                hasBeenThrown = true;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasBeenThrown) // If the weapon collides with an enemy and the weapon has been thrown
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
