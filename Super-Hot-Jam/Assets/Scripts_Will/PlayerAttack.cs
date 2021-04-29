using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Setup")]
    public MeleeWeapon weaponSettings;
    public GameObject hitPoint;
    public bool weaponActive;
    public bool hasBeenThrown;
    public ParticleSystem weaponDestroyParticles;
    public ParticleSystem enemyDestroyParticles;

    [SerializeField]private float attackTimer; // Time until next hit
    [SerializeField]private float throwTimer;
    private float hitsLeft; // Number of hits left in the weapon

    public AudioController audio;

    Rigidbody2D rb;
    PlayerMovement player;
    Animator anim;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        hitsLeft = weaponSettings.hitCount;

        GameObject audioControl = GameObject.FindGameObjectWithTag("Audio");
        audio = audioControl.GetComponent<AudioController>();
    }

    void Update()
    {
        if (weaponActive)
        {
            Attack();
            ThrowWeapon();
        }
    }

    void Attack()
    {
        attackTimer -= Time.deltaTime; // Reduce the time until next shot is available

        // Get all colliders around player within range of the weapon reach
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(hitPoint.transform.position, weaponSettings.weaponReach);

        if (attackTimer <= 0)
        {
            if (Input.GetButtonDown("Fire1") && hitsLeft != 0)
            {
                foreach (Collider2D collidedObject in collidersInRange)
                {
                    if (collidedObject.CompareTag("Enemy"))
                    {
                        Destroy(collidedObject.gameObject);
                        audio.meleeHit = true;
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
            Instantiate(weaponDestroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void ThrowWeapon()
    {
        if (Input.GetButton("Fire1"))
        {
            throwTimer += Time.unscaledDeltaTime; // Begin the timer for throwing a gun
        }

        if (throwTimer >= weaponSettings.throwTime)  // If the timer is complete
        {
            if (Input.GetButtonUp("Fire1")) // If the player releases the A button
            {
                rb.AddRelativeForce(new Vector2(0, -weaponSettings.throwForce * Time.unscaledDeltaTime), ForceMode2D.Impulse); // Apply a force to the weapon
                rb.AddTorque(weaponSettings.throwTorque * Time.unscaledDeltaTime, ForceMode2D.Impulse);
                anim.enabled = false;
                player.weaponEquipped = false;

                transform.SetParent(null); // Removes the parent from the weapon

                this.GetComponent<PickupMeleeWeapon>().isParented = false;

                throwTimer = 0;
                hasBeenThrown = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Collisions")) && hasBeenThrown) // If the weapon collides with an enemy and the weapon has been thrown
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(weaponDestroyParticles, transform.position, transform.rotation);
            audio.meleeHit = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint.transform.position, weaponSettings.weaponReach);
    }
}
