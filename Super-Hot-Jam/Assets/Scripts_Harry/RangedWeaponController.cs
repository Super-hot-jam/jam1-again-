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
    private float shotCounter;
    
    Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        currentAmmo = weaponSettings.ammoCount;
    }

    private void Update()
    {
        FireWeapon();
    }

    void FireWeapon()
    {
        shotCounter -= Time.deltaTime; // Reduce the time until next shot is available

        if (Input.GetButtonDown("Fire1") && currentAmmo != 0 && weaponActive)
        {
            if (shotCounter <= 0) // If next shot is ready
            {
                if (!weaponSettings.hasSpread) // If the weapon doesn't have a spread (i.e not a shotgun)
                {
                    anim.SetTrigger("Firing");

                    Instantiate(projectile, firePoint.position, firePoint.rotation);

                    shotCounter = weaponSettings.fireRate; // Resets shot counter dependant on the weapons fire rate
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

                    shotCounter = weaponSettings.fireRate; // Resets shot counter dependant on the weapons fire rate
                    currentAmmo--;
                }
            }
        }
    }
}
