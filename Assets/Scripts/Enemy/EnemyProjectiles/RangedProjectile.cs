// Handles logic for a ranged enemy projectile including movement, lifetime, collision, and applying damage to the player.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    private float damage;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private Rigidbody rb; 

    // Sets the damage value for the projectile.
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    // Destroys the projectile after its lifetime expires.
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Checks collision with player, applies damage if hit, then destroys the projectile.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
                hp.TakeDamage(damage);

            Destroy(gameObject);
        }
        
    }

    // Returns the rigidbody reference for external control (e.g., velocity/force).
    public Rigidbody GetRigidbody()
    {
        return rb;
    }
}
