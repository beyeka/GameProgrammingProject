using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    private float damage;
    [SerializeField] private float lifeTime = 5f;

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
        
    }
}
