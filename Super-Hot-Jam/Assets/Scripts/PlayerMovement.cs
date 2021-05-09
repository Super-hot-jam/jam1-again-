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

    Component player_combat;

    float hori;
    float verti;
    Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player_combat = GetComponent<PlayerAttack>();
    }

   void Update()
    {
        hori = Input.GetAxis("left_joyhori");
        verti = Input.GetAxis("left_joyverti");
        movementDirection = new Vector2(hori * speed, -verti * speed);
        //Debug.Log(movementDirection);
        SlowMoving();
        Combat();
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * Time.deltaTime;

        if (movementDirection.y >= 2500 || movementDirection.y <= -2500 || movementDirection.x >= 2500 || movementDirection.x <= -2500) // Dead zones
        {
            isMoving = true;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        }

        else if(movementDirection == Vector2.zero)
        {
            isMoving = false;
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

    void Combat()
    {
        if (Input.GetButtonDown("Fire1") & equiped_player_weapon != null)//if 'a' (controller) is pressed
        {
            Debug.Log("reqiesting attack - fire button pressed and equipped weapon is: " + equiped_player_weapon.name);
            equiped_player_weapon.GetComponent<Attack>().WeaponAttack("Enemy");
        }
    }

    void SlowMoving()
    {
        if(!isMoving)
        {
             timeManager.SlowTime();
        }

        if(isMoving)
        {
            timeManager.SpeedupTime();
        }    
    }

    void OnKill()
    {
        if (deathAnim != null)
        {
            GameObject.Instantiate(deathAnim, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject, 0.2f);
        }
    }
}

