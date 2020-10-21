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
                Instantiate(projectile, firePoint.position, transform.rotation);
                //anim.SetTrigger("Firing");
            }
        }

        if(weaponSettings.hasSpread)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                float TotalSpread = weaponSettings.spreadAngle / weaponSettings.projectileCount;

                for (int i = 0; i < weaponSettings.projectileCount; i++)
                {
                    // Calculate angle of this bullet
                    float spreadA = TotalSpread * (i + 1);
                    float spreadB = weaponSettings.spreadAngle / 2.0f;
                    float spread = spreadB - spreadA + TotalSpread / 2;
                    float angle = transform.eulerAngles.y;

                    // Create rotation of bullet
                    Quaternion rotation = Quaternion.Euler(new Vector3(0, spread + angle, 0));

                    // Create bullet
                    GameObject bullet = Instantiate(projectile);
                    bullet.transform.position = firePoint.position;
                    bullet.transform.rotation = rotation;
                }
            }
        }
    }

}
