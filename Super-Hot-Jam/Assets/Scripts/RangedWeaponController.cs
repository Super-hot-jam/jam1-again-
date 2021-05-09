using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    [Header("Setup")]
    public RangedWeaponSO weaponSettings; // Get the weapons attributes and settings
    public GameObject projectile;
    public Transform firePoint;
    public bool weaponActive;
    public bool hasBeenThrown;
    public ParticleSystem destroyParticles;

    public AudioController audio; //get audio controller

    [SerializeField] private float currentAmmo = 0;

    private float shotTimer; // Time until next shot
    private float throwTimer;
    
    Animator anim;
    Rigidbody2D rb;
    PlayerMovement player;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        currentAmmo = weaponSettings.ammoCount;

        GameObject audioControl = GameObject.FindGameObjectWithTag("Audio");
        audio = audioControl.GetComponent < AudioController >(); //finds audio controller
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
        if (GetComponentInParent<Enemy_AI>() != null)
        {

        }
        else
        {
            if (Input.GetButtonDown("Fire1") && currentAmmo != 0)
            {
                if (shotTimer <= 0) // If next shot is ready
                {
                    // PISTOL/SINGLE SHOT FUNCTIONALITY
                    if (!weaponSettings.hasSpread) // If the weapon doesn't have a spread (i.e not a shotgun)
                    {
                        anim.SetTrigger("Firing");

                        Instantiate(projectile, firePoint.position, firePoint.rotation);

                    shotTimer = weaponSettings.fireRate; // Resets shot counter dependant on the weapons fire rate
                    currentAmmo--;

                    audio.pistolPlay = true; //play pistol sound
                }

                    // SHOTGUN/SPREAD WEAPON FUNCTIONALITY
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

                    shotTimer = weaponSettings.fireRate; // Resets shot counter dependant on the weapons fire rate
                    currentAmmo--;

                    audio.shotgunPlay = true; //play shotgun sound
                }
            }
        }
    }

    void ThrowWeapon()
    {
        if(GetComponentInParent<Enemy_AI>() != null)
        {

        }
        else
        {
            if (currentAmmo <= 0)
            {
                if (Input.GetButton("Fire1"))
                {
                    throwTimer += Time.unscaledDeltaTime; // Begin the timer for throwing a gun
                }

                if (throwTimer >= weaponSettings.throwTime)  // If the timer is complete
                {
                    if (Input.GetButtonUp("Fire1")) // If the player releases the A button
                    {
                        rb.AddRelativeForce(new Vector2(0, -weaponSettings.throwForce * Time.unscaledDeltaTime), ForceMode2D.Impulse); // Apply a force to the weapon
                        rb.AddTorque(weaponSettings.throwTorque * Time.unscaledDeltaTime, ForceMode2D.Impulse);

                        //player.weaponEquipped = false;

                        transform.SetParent(null); // Removes the parent from the weapon

                        this.GetComponent<PickupWeapon>().isParented = false;

                        throwTimer = 0;
                        hasBeenThrown = true;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Collisions")) && hasBeenThrown) // If the weapon collides with an enemy and the weapon has been thrown
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(destroyParticles, transform.position, transform.rotation);

            audio.enemyKill = true;
        }
    }
}
