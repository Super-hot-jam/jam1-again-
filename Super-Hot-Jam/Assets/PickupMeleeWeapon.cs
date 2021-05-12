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
    private LevelManager level;

    private void Start()
    {
        level = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        level.LevelWeapons.Add(gameObject);
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        SetPosOfEquippedWeapon();

        if (!this.gameObject.GetComponent<Attack>().hasBeenThrown)
        {
            if (level.EquippedWeapons.Contains(gameObject) == false)//if weapon not equipped to a character
            {
                Collider2D[] overlappedColldiers = Physics2D.OverlapCircleAll(transform.position, weaponSettings.pickupRadius); // Check all colliders in a radius for valid equip location 
                foreach (Collider2D hitCollider in overlappedColldiers)
                {
                    if (hitCollider.gameObject.CompareTag("GunPoint"))//if equip location found
                    {
                        if (hitCollider.gameObject.GetComponentInParent<PlayerMovement>() != null)//if player
                        {
                            if(hitCollider.gameObject.GetComponentInParent<PlayerMovement>().HasWeapon() == false)
                            {
                                hitCollider.gameObject.GetComponentInParent<PlayerMovement>().SetCurrentWeapon(gameObject);
                                transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                                isParented = true;
                                gameObject.GetComponent<Attack>().SetWielder(hitCollider.gameObject.transform.parent.gameObject);
                                gameObject.GetComponent<Attack>().weaponActive = true;
                                level.EquippedWeapons.Add(gameObject);
                                break;
                            }
                        }

                        if (hitCollider.gameObject.GetComponentInParent<Enemy_AI>() != null)//if AI
                        {
                            if (hitCollider.gameObject.GetComponentInParent<Enemy_AI>().HasWeapon() == false)
                            {
                                hitCollider.gameObject.GetComponentInParent<Enemy_AI>().SetCurrentWeapon(gameObject);
                                transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the AI's gun point object
                                gameObject.GetComponent<Rigidbody2D>().simulated = false;
                                isParented = true;
                                gameObject.GetComponent<Attack>().SetWielder(hitCollider.gameObject.transform.parent.gameObject);
                                gameObject.GetComponent<Attack>().weaponActive = true;
                                level.EquippedWeapons.Add(gameObject);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    void SetPosOfEquippedWeapon()
    {
        if(isParented)
        {
            transform.position = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
