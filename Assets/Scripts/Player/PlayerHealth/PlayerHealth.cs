using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    private float health;

    private void Start()
    {
        health = maxHealth;
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health == 0)
        {
            Die();
        }
    }

    
    public void Die()
    {
        
        
    }
    
    
}
