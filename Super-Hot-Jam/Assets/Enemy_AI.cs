using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI : MonoBehaviour
{
    public enum State
    {
        NullState = 0,
        GoToPlayerState = 1,
        GoToGunState = 2,
        ShootState = 3,
        MeleeState = 4
    }

    public GameObject player;

    public State state = 0;
    public float enemy_collider = 0.5f;
    public float shoot_range = 10f;
    public bool hasWeapon;
    private bool playerSeen;
    private bool gunReloaded;
    public float distFromPlayer;
    

    public LayerMask weaponMask;
    public LayerMask environmentMask;

    public List<Transform> visibleWeapons = new List<Transform>();
   

    //Navigation parameters
    public Transform seekTarget;
    AIDestinationSetter setter;

    //Debug variables
    public bool playerRayDebugging = true;
    public bool weaponsDebugging = true;

    private void Start()
    {
        state = State.GoToPlayerState;
        setter = GetComponent<AIDestinationSetter>();
        seekTarget = setter.target;
    }

    private void Update()
    {

        // Update state transition data
        //.. distance from player and 
        distFromPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        if (!Physics.Raycast(this.transform.position, direction, shoot_range, environmentMask))
        {
            // can see player & within shooting range
            playerSeen = true;
        }
        else
        {
            playerSeen = false;
        }

        //.. get all visible weapons
        GetVisibleWeapons();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (visibleWeapons.Count != 0)
            {
                setter.target = visibleWeapons[0];
                seekTarget = visibleWeapons[0];
            }
        }


        // Perform state code
        switch (state)
        {
            case State.GoToPlayerState:
                setter.target = player.transform;

                //go to gun transition is a gun is closer than the player
                if(GetClosestWeapon() != null && !hasWeapon)
                {
                    if(Vector2.Distance(this.transform.position, GetClosestWeapon().position) < distFromPlayer && !(hasWeapon))
                    {
                        state = State.GoToGunState;
                    }
                }

                //shoot transition if has weapon, player seen and gun is reloaded
                if(hasWeapon && playerSeen && gunReloaded)
                {
                    state = State.ShootState;
                }


                
                break;
            case State.GoToGunState:
                setter.target = GetClosestWeapon();

                //pick up gun code
                if (Vector2.Distance(setter.target.position, this.transform.position) < 1f)
                {
                    // TODO code for picking up gun
                    hasWeapon = true;
                }

                //shoot transition if has weapon and player seen
                if (hasWeapon && playerSeen)
                {
                    state = State.ShootState;
                }
                //go to player if has weapon and player not in sight
                else if (hasWeapon && !playerSeen)
                {
                    state = State.GoToPlayerState;
                }
         

                break;
            case State.ShootState:
                // TODO code for shooting gun at player need harrys code


                state = State.GoToPlayerState;
                
                break;
            case State.MeleeState:
                // TODO code for melee need harrys code too
                
                
                break;
            default:
                break;
        }


    }

    private Transform GetClosestWeapon()
    {
        Transform closestWeapon = null;
        if (visibleWeapons.Count == 0)
            return closestWeapon;
        else
        {
            foreach (Transform w in visibleWeapons)
            {
                if (closestWeapon == null)
                    closestWeapon = w;
                else
                if (Vector2.Distance(this.transform.position, w.position) < Vector2.Distance(this.transform.position, closestWeapon.position))
                    closestWeapon = w;
            }
            return closestWeapon;
        }
    }


    private void GetVisibleWeapons()
    {
        visibleWeapons.Clear();

        Collider2D[] weaponsInRange = Physics2D.OverlapCircleAll(this.transform.position, 5f, weaponMask);

        for (int i = 0; i < weaponsInRange.Length; i++)
        {
            Transform weapon = weaponsInRange[i].transform;
            Vector3 dir = (weapon.position - this.transform.position).normalized;
            if (!Physics.Raycast(this.transform.position, dir, 5f, environmentMask))
            {
                visibleWeapons.Add(weapon);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerRayDebugging)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, enemy_collider);

            // ray towards player
            if (playerSeen)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, player.transform.position);
        }

        

        if (weaponsDebugging)
        {

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, 5f);
            foreach (Transform w in visibleWeapons)
            {
                Gizmos.DrawLine(this.transform.position, w.position);
            }
        }

    }
}
