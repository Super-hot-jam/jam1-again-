using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /*private void FixedUpdate()
    {
        if (!this.gameObject.GetComponent<Attack>().hasBeenThrown)
        {
            positionSet = false;
            Collider2D[] overlappedColldiers = Physics2D.OverlapCircleAll(transform.position, weaponSettings.pickupRadius); // Check all colliders in a radius

            foreach (Collider2D hitCollider in overlappedColldiers)
            {
                if (hitCollider.gameObject.CompareTag("GunPoint"))
                {
                    if (!isParented/* && !player.weaponEquipped*//*)
                    {
                        this.transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                        isParented = true;
                    }

                    if (isParented && !positionSet)
                    {
                        SetPosOfEquippedWeapon();

                        this.gameObject.GetComponent<Attack>().weaponActive = true; // Set the weapon to be active (usable)

                        Transform parent = hitCollider.gameObject.transform.parent;
                        if (parent.GetComponent<PlayerMovement>() != null & !registeredWeapon)
                        {
                            if (parent.GetComponent<PlayerMovement>().HasWeapon() == false)
                            {
                                parent.GetComponent<PlayerMovement>().SetCurrentWeapon(gameObject);
                                registeredWeapon = true;
                            }
                        }
                        else if (parent.GetComponent<Enemy_AI>() != null & !registeredWeapon)
                        {
                            if (parent.GetComponent<Enemy_AI>().HasWeapon() == false)
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

    void SetPosOfEquippedWeapon()
    {
        transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 180);
    }*/
}
