using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4;
    public TimeManager timeManager;
    public bool isMoving = false;
    public bool weaponEquipped;
    public GameObject deathAnim;

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
        Debug.Log(movementDirection);
        SlowMoving();
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

