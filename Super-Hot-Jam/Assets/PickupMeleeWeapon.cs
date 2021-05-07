using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMeleeWeapon : MonoBehaviour
{
    [Header("Setup")]
    public MeleeWeapon weaponSettings;
    public bool isParented;

    PlayerMovement player;

    private bool positionSet = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!this.gameObject.GetComponent<PlayerAttack>().hasBeenThrown)
        {
            positionSet = false;
            Collider2D[] overlappedColldiers = Physics2D.OverlapCircleAll(transform.position, weaponSettings.pickupRadius); // Check all colliders in a radius

            foreach (Collider2D hitCollider in overlappedColldiers)
            {
                if (hitCollider.gameObject.CompareTag("GunPoint"))
                {
                    if (!isParented && !player.weaponEquipped)
                    {
                        this.transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                        Debug.Log("parenting to object");
                        isParented = true;
                    }

                    if (isParented && !positionSet)
                    {
                        //Debug.Log("setting transform");
                        transform.position = Vector3.zero;
                        transform.localPosition = Vector3.zero;
                        //transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 50 * Time.unscaledDeltaTime); // Position to origin of gunpoint
                        transform.localRotation = Quaternion.Euler(0, 0, 180); // Maintain a forward rotation

                        this.gameObject.GetComponent<PlayerAttack>().weaponActive = true; // Set the weapon to be active (usable)
                        player.weaponEquipped = true; // Set it so the player has a weapon equipped, avoids picking up multiple

                        positionSet = true;
                    }
                }
            }
        }
    }
}
