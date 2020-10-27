using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [Header("Setup")]
    public RangedWeaponSO weaponSettings;
    public bool isParented;

    PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!this.gameObject.GetComponent<RangedWeaponController>().hasBeenThrown)
        {
            Collider2D[] overlappedColldiers = Physics2D.OverlapCircleAll(transform.position, weaponSettings.pickupRadius); // Check all colliders in a radius

            foreach (Collider2D hitCollider in overlappedColldiers)
            {
                if (hitCollider.gameObject.CompareTag("GunPoint"))
                {
                    if (!isParented && !player.weaponEquipped)
                    {
                        this.transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                        isParented = true;
                    }

                    if (isParented)
                    {
                        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 50 * Time.unscaledDeltaTime); // Position to origin of gunpoint
                        transform.localRotation = Quaternion.Euler(0, 0, 180); // Maintain a forward rotation

                        this.gameObject.GetComponent<RangedWeaponController>().weaponActive = true; // Set the weapon to be active (firable)
                        player.weaponEquipped = true; // Set it so the player has a weapon equipped, avoids picking up multiple
                    }
                }
            }
        }
    }
}
