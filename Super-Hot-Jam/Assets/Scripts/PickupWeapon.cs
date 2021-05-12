using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [Header("Setup")]
    public RangedWeaponSO weaponSettings;
    public bool isParented;

    PlayerMovement player;
    private LevelManager level;

    private void Start()
    {
        level = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        level.LevelWeapons.Add(gameObject);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        SetPosOfEquippedWeapon();

        if (!this.gameObject.GetComponent<RangedWeaponController>().hasBeenThrown)
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
                            if (hitCollider.gameObject.GetComponentInParent<PlayerMovement>().HasWeapon() == false)
                            {
                                hitCollider.gameObject.GetComponentInParent<PlayerMovement>().SetCurrentWeapon(gameObject);
                                transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                                isParented = true;
                                gameObject.GetComponent<RangedWeaponController>().weaponActive = true;
                                //gameObject.GetComponent<Attack>().SetWielder(hitCollider.gameObject.transform.parent.gameObject);
                                //gameObject.GetComponent<Attack>().weaponActive = true;
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
                                gameObject.GetComponent<RangedWeaponController>().weaponActive = true; // Set the weapon to be active (firable)
                                //gameObject.GetComponent<Attack>().SetWielder(hitCollider.gameObject.transform.parent.gameObject);
                                //gameObject.GetComponent<Attack>().weaponActive = true;
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
        if (isParented)
        {
            transform.position = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }

    /*private void Update()
    {
        if (!this.gameObject.GetComponent<RangedWeaponController>().hasBeenThrown)
        {
            Collider2D[] overlappedColldiers = Physics2D.OverlapCircleAll(transform.position, weaponSettings.pickupRadius); // Check all colliders in a radius

            foreach (Collider2D hitCollider in overlappedColldiers)
            {
                if (hitCollider.gameObject.CompareTag("GunPoint"))
                {


                    if (hitCollider.gameObject.GetComponentInParent<Enemy_AI>() != null)
                    {
                        Enemy_AI enemy = hitCollider.gameObject.GetComponentInParent<Enemy_AI>();
                        if (!isParented && !enemy.hasWeapon)
                        {
                            this.transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                            isParented = true;
                        }
                        if (isParented)
                        {
                            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 50 * Time.unscaledDeltaTime); // Position to origin of gunpoint
                            transform.localRotation = Quaternion.Euler(0, 0, 180); // Maintain a forward rotation

                            this.gameObject.GetComponent<RangedWeaponController>().weaponActive = true; // Set the weapon to be active (firable)
                            enemy.hasWeapon = true;
                        }
                    }
                    else if (hitCollider.GetComponentInParent<PlayerMovement>() != null)
                    {

                        if (!isParented)
                        {
                            this.transform.SetParent(hitCollider.gameObject.transform); // Set the parent to the player's gun point object
                            isParented = true;
                        }

                        if (isParented)
                        {
                            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 50 * Time.unscaledDeltaTime); // Position to origin of gunpoint
                            transform.localRotation = Quaternion.Euler(0, 0, 180); // Maintain a forward rotation

                            this.gameObject.GetComponent<RangedWeaponController>().weaponActive = true; // Set the weapon to be active (firable)
                            //player.weaponEquipped = true; // Set it so the player has a weapon equipped, avoids picking up multiple
                        }
                    }
                }
                }
            }
        }*/
    }

