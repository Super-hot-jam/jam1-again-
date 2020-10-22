using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public MeleeWeapon currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        //get all colliders around player within range of the weapon reach
        //Collider[] collidersInRange = Physics.OverlapSphere(transform.position, currentWeapon.weaponReach);
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, currentWeapon.weaponReach);
        if (collidersInRange.Length > 0)
        {
            Debug.Log("Someone in range");
            Debug.Log("Num of people: " + collidersInRange.Length);
            Debug.Log(collidersInRange[0].name);
        }
        else
        {
            Debug.Log("no one in range");
        }

        //goes through all objects in range of player
        for (int i = 0; i < collidersInRange.Length; i++)
        {
            //Calculate the vector from the player to the collider in question
            Vector2 vectorToCollider = (collidersInRange[i].transform.position - transform.position);
            vectorToCollider = vectorToCollider.normalized;

            //if the object is in front of player 
            if (Vector2.Dot(vectorToCollider, transform.forward) > 0)
            {
                Debug.Log("Deal Damage");
                //damage bad guy
            }
        }      
    }
}
