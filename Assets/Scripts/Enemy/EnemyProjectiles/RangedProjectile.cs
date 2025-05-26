using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    private float damage;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private Rigidbody rb; 

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
                hp.TakeDamage(damage);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }
}
