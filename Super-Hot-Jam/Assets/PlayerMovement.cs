using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb;
    public float speed = 4;
    private Vector3 origpos;
    float hori;
    float verti;
    Vector2 Look;
    // Start is called before the first frame update
    void Start()
    {
        origpos = transform.position;
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
 
   public void Movement()
    {



       Vector2 moveinput = new Vector2(Input.GetAxis("left_joyhori"), Input.GetAxis("left_joyverti"));



        Vector2 forward = transform.up;
        Vector2 right = transform.right;

        forward.x = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 DesMoveDir = (forward * -moveinput.y + right * moveinput.x).normalized;

        Rb.MovePosition(transform.position + DesMoveDir * (speed * Time.deltaTime));

        if (DesMoveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, DesMoveDir);
        }
    }
   void Update()
    {
        hori = Input.GetAxis("left_joyhori");
        verti = Input.GetAxis("left_joyverti");
        Look = new Vector2(hori * speed, -verti * speed);
    }
    private void FixedUpdate()
    {

        //Movement();
        Rb.velocity = Look;
        if (Look != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Look);
        }

    }
  
}
