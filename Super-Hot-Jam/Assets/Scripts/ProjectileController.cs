using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Setup")]
    public ProjectileSO projectileSettings; // Get the projectiles attributes and settings

    Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = -transform.up * projectileSettings.speed * Time.deltaTime; // Moves the projectile
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("yesa");
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy_AI>().OnKill();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("yesa");
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy_AI>().OnKill();

        }
    }
}
