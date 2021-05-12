using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4;
    public TimeManager timeManager;
    public bool isMoving = false;
    //public bool weaponEquipped;
    public GameObject deathAnim;

    GameObject equiped_player_weapon;

    private LevelManager level;


    float hori;
    float verti;
    Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   void Update()
    {
        hori = Input.GetAxis("left_joyhori");
        verti = Input.GetAxis("left_joyverti");
        movementDirection = new Vector2(hori * speed, -verti * speed);
        level = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        //Debug.Log(movementDirection);
        SlowMoving();
        Combat();
    }

    private void FixedUpdate()
    {
        try
        {
            if (!level.SceneTransitioning())
            {
                rb.velocity = movementDirection * Time.deltaTime;

                if (movementDirection.y >= 2500 || movementDirection.y <= -2500 || movementDirection.x >= 2500 || movementDirection.x <= -2500) // Dead zones
                {
                    isMoving = true;
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
                }

                else if (movementDirection == Vector2.zero)
                {
                    isMoving = false;
                }
            }
        }
        catch
        {
            Debug.LogWarning("referencing scene that doesnt exist?");
        }
        
    }

    public void SetCurrentWeapon(GameObject weapon)//used to set new weapon
    {
        equiped_player_weapon = weapon;
    }
    public void SetCurrentWeapon()//used for removing weapon
    {
        equiped_player_weapon = null;
    }
    public bool HasWeapon()
    {
        if(equiped_player_weapon == null)
        {
            return false;
        }

        return true;
    }

    void Combat()
    {
        if (Input.GetButtonDown("Fire1") && equiped_player_weapon != null)//if 'a' (controller) is pressed
        {
            try 
            {
                equiped_player_weapon.GetComponent<Attack>().WeaponAttack("Enemy");
            }
            catch { }
            
        }

        if (Input.GetButtonDown("Fire2") && equiped_player_weapon != null)//if 'a' (controller) is pressed
        {
            
            try 
            {
                equiped_player_weapon.GetComponent<Attack>().ThrowWeapon();
            }
            catch 
            {
                //Debug.Log("Trying to throw");
            }
            
        }
    }

    void SlowMoving()
    {
        if(!isMoving && !level.SceneTransitioning())
        {
             timeManager.SlowTime();
        }

        if(isMoving)
        {
            timeManager.SpeedupTime();
        }    
    }

    public void OnKill()
    {
        if (deathAnim != null)
        {
            GameObject.Instantiate(deathAnim, this.transform.position, this.transform.rotation);
            
        }
        Destroy(this.gameObject, 0.2f);

        LevelManager level_manager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        level_manager.GameOver();
    }
}

