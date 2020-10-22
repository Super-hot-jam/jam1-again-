using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4;
    float hori;
    float verti;
    Vector2 movementDirection;
    public TimeManager timeManager;
    public bool isMoving = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   void Update()
    {
        hori = Input.GetAxis("left_joyhori");
        verti = Input.GetAxis("left_joyverti");
        movementDirection = new Vector2(hori * speed, -verti * speed);

        SlowMoving();
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * Time.deltaTime;

        if (movementDirection != Vector2.zero)
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
}
