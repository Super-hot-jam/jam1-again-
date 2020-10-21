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
        // KILL ENEMY
        // Destroy(gameObject);
    }
}
