using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb;
    public float speed = 4;
    float hori;
    float verti;
    Vector2 Look;
    public TimeManager timemanager;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {

        Rb = GetComponent<Rigidbody2D>();
    }

   void Update()
    {
        hori = Input.GetAxis("left_joyhori");
        verti = Input.GetAxis("left_joyverti");
        Look = new Vector2(hori * speed, -verti * speed);
        slowmoving();
    }
    private void FixedUpdate()
    {

        Rb.velocity = Look;
        if (Look != Vector2.zero)
        {
            isMoving = true;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Look);
        }
        else if(Look == Vector2.zero)
        {
            isMoving = false;
        }
        

    }
    void slowmoving()
    {
        if(!isMoving)
        {
          
             timemanager.SlowTime();
            
        }
        if(isMoving)
        {
            timemanager.speedupTime();
        }    
     
    }
  
}
