using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMeleeWeapon : MonoBehaviour
{
    [Header("Setup")]
    public MeleeWeapon weaponSettings;
    public bool isParented;

    //PlayerMovement player;

    private bool positionSet = false;
    private bool registeredWeapon = false;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (!this.gameObject.GetComponent<Attack>().hasBeenThrown)
        {
            positionSet = false;
            Collider2D[] overlappedColldiers = Physics2D.OverlapCircleAll(transform.position, weaponSettings.pickupRadius); // Check all colliders in a radius

            foreach (Collider2D hitCollider in overlappedColldiers)
            {
                if (hitCollider.gameObject.CompareTag("GunPoint"))
                {
                    if (!isParented/* && !player.weaponEquipped*/)
                    {
                        this.transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                        isParented = true;
                    }

                    if (isParented && !positionSet)
                    {
                        //Debug.Log("setting transform");
                        transform.position = Vector3.zero;
                        transform.localPosition = Vector3.zero;
                        //transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 50 * Time.unscaledDeltaTime); // Position to origin of gunpoint
                        transform.localRotation = Quaternion.Euler(0, 0, 180); // Maintain a forward rotation

                        this.gameObject.GetComponent<Attack>().weaponActive = true; // Set the weapon to be active (usable)
                        //player.weaponEquipped = true; // Set it so the player has a weapon equipped, avoids picking up multiple
                        //player.SetCurrentWeapon(gameObject);
                        Transform parent = hitCollider.gameObject.transform.parent;
                        if (parent.GetComponent<PlayerMovement>() != null & !registeredWeapon)
                        {
                            if(parent.GetComponent<PlayerMovement>().HasWeapon() == false)
                            {
                                parent.GetComponent<PlayerMovement>().SetCurrentWeapon(gameObject);
                                registeredWeapon = true;
                            }                        
                        }
                        else if(parent.GetComponent<Enemy_AI>() != null & !registeredWeapon)
                        {
                            if(parent.GetComponent<Enemy_AI>().HasWeapon() == false)
                            {
                                parent.GetComponent<Enemy_AI>().SetCurrentWeapon(gameObject);
                                Debug.Log("rb simulated set to false");
                                gameObject.GetComponent<Rigidbody2D>().simulated = false;
                                registeredWeapon = true;
                            } 
                        }
                        gameObject.GetComponent<Attack>().SetWielder(parent.gameObject);
                        gameObject.GetComponent<Attack>().weaponActive = true;
                        positionSet = true;
                    }
                }
            }
        }
    }
}
