using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Setup")]
    public MeleeWeapon weaponSettings;
    public GameObject hitPoint;
    public bool weaponActive;
    public bool hasBeenThrown;

    [SerializeField] private float attackTimer; // Time until next hit
    [SerializeField] private float throwTimer;
    private float hitsLeft; // Number of hits left in the weapon

    Rigidbody2D rb;
    GameObject wielder = null;
    Animator anim;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        //Wielder = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        hitsLeft = weaponSettings.hitCount;

        GameObject audioControl = GameObject.FindGameObjectWithTag("Audio");
        //audio = audioControl.GetComponent<AudioController>();
    }

    void Update()
    {

        if (weaponActive)
        {
            //Attack();
            attackTimer -= Time.deltaTime; // Reduce the time until next shot is available
            //ThrowWeapon();
        }
    }

    public void SetWielder(GameObject character)
    {
        wielder = character;
        if(wielder.GetComponent<Enemy_AI>() == null && wielder.GetComponent<PlayerMovement>() == null)
        {
            Debug.LogWarning("No wielder is not valid setting wielder to null");
            wielder = null;
        }
    }

    /*void Attack()
    {
        attackTimer -= Time.deltaTime; // Reduce the time until next shot is available

        // Get all colliders around player within range of the weapon reach
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(hitPoint.transform.position, weaponSettings.weaponReach);

        if (attackTimer <= 0)
        {
            if (Input.GetButtonDown("Fire1") && hitsLeft != 0)//if 'a' (controller) is pressed and item has hits left
            {
                Debug.Log("should be attacking anything in range - a has been pressed");
                foreach (Collider2D collidedObject in collidersInRange)
                {
                    if (collidedObject.CompareTag("Enemy"))
                    {
                        Destroy(collidedObject.gameObject);
                        //audio.meleeHit = true;
                    }
                }

                anim.SetTrigger("Swinging");
                attackTimer = weaponSettings.hitRate;
                hitsLeft--;
            }
        }

        if (hitsLeft <= 0)
        {
            player.weaponEquipped = false;
            //Instantiate(weaponDestroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }*/

    public void WeaponAttack(string target)
    {
        if (weaponActive)
        {
            // Get all colliders around player within range of the weapon reach
            Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(hitPoint.transform.position, weaponSettings.weaponReach);

            if (attackTimer <= 0)
            {
                foreach (Collider2D collidedObject in collidersInRange)
                {
                    /*if(target == wielder.tag)
                    {
                        Debug.LogWarning("targeting wielder for some reason rip... setting to null target");
                        target = null;
                    }*/
                    if (collidedObject.CompareTag(target))
                    {
                        if(collidedObject.GetComponent<Enemy_AI>() != null)
                        {
                            collidedObject.GetComponent<Enemy_AI>().OnKill();
                        }
                        else
                        {
                            collidedObject.GetComponent<PlayerMovement>().OnKill();
                        }
                        
                        //audio.meleeHit = true;
                    }
                }

                anim.SetTrigger("Swinging");
                attackTimer = weaponSettings.hitRate;
                hitsLeft--;
            }

            if (hitsLeft <= 0)
            {
                //player.weaponEquipped = false;
                if(wielder.GetComponent<PlayerMovement>() != null)
                {
                    wielder.GetComponent<PlayerMovement>().SetCurrentWeapon();
                }
                else
                {
                    wielder.GetComponent<Enemy_AI>().SetCurrentWeapon();
                }


                //player.GetComponent.SetCurrentWeapon();
                //Instantiate(weaponDestroyParticles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }




    public void ThrowWeapon()
    {
        /*if (Input.GetButton("Fire2"))//if 'b' (controller) is pressed
        {
            Debug.Log("begin timer for throw weapon - b has been pressed");
            throwTimer += Time.unscaledDeltaTime; // Begin the timer for throwing a gun
        }*/

        Debug.Log("weapon is thrown - b has been released");
        rb.AddRelativeForce(new Vector2(0, -weaponSettings.throwForce * Time.unscaledDeltaTime), ForceMode2D.Impulse); // Apply a force to the weapon
        rb.AddTorque(weaponSettings.throwTorque * Time.unscaledDeltaTime, ForceMode2D.Impulse);
        anim.enabled = false;
        //player.weaponEquipped = false;
        if (wielder.GetComponent<PlayerMovement>() != null)
        {
            wielder.GetComponent<PlayerMovement>().SetCurrentWeapon();
        }
        else
        {
            wielder.GetComponent<Enemy_AI>().SetCurrentWeapon();
        }
        //player.SetCurrentWeapon();

        transform.SetParent(null); // Removes the parent from the weapon

        this.GetComponent<PickupMeleeWeapon>().isParented = false;

        throwTimer = 0;
        hasBeenThrown = true;
        weaponActive = false;

        /*if (throwTimer >= weaponSettings.throwTime)  // If the timer is complete
        {
           
            /*if (Input.GetButtonUp("Fire2")) // If the player releases the A button
            {
                
            }*/
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") /*|| collision.gameObject.CompareTag("Collisions"))*/ && hasBeenThrown) // If the weapon collides with an enemy and the weapon has been thrown
        {
            collision.transform.GetComponent<Enemy_AI>().OnKill();
            //Destroy(collision.gameObject);
            Destroy(gameObject);
            //Instantiate(weaponDestroyParticles, transform.position, transform.rotation);
            //audio.meleeHit = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint.transform.position, weaponSettings.weaponReach);
    }
}
