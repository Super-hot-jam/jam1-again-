using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb;
    public float speed = 4;
    private Vector3 origpos;
    // Start is called before the first frame update
    void Start()
    {
        origpos = transform.position;
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
 
    void Movement()
    {



        Vector2 moveinput = new Vector2(Input.GetAxis("left_joyhori"), Input.GetAxis("left_joyverti"));

        Vector2 forward = transform.up;
        Vector2 right = transform.right;

        forward.x = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 DesMoveDir = (forward * -moveinput.y + right * moveinput.x).normalized;

        Rb.MovePosition( transform.position + DesMoveDir * (speed * Time.deltaTime));

      
    }
    void Update()
    {
        Movement();
        //Vector2 Look = gameObject.GetComponent<Rigidbody2D>().velocity;
        //if (Look != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }
}
