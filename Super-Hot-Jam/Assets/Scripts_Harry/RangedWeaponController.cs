using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    [Header("Setup")]
    public RangedWeaponSO weaponSettings; // Get the weapons attributes and settings
    public GameObject projectile;
    public Transform firePoint;
    
    Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        FireWeapon();
    }

    void FireWeapon()
    {
        if (!weaponSettings.hasSpread) // If the weapon doesn't have a spread (i.e not a shotgun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                //anim.SetTrigger("Firing");
            }
        }

        if (weaponSettings.hasSpread) // If the weapon has spread (i.e is a shotgun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                for (int i = 0; i < weaponSettings.projectileCount; i++)
                {
                    Instantiate(projectile, firePoint.position, firePoint.rotation);
                }
            }
        }
    }
    
}
