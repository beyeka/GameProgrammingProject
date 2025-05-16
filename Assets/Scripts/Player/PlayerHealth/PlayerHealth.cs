using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{   
    [SerializeField]private float health;
    private float lerpTimer;
    public float maxHealth;
    public float chipSpeed =2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;
        
    private void Start()
    {
        health = maxHealth;
    }

     void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(25);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RestoreHealth(25);
        }
    }

    public void UpdateHealthUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillBack>hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }

        if (fillFront < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }

        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);   
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        lerpTimer = 0f;
        if (health == 0)
        {
            Die();
        }
    }

    public void RestoreHealth(float healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
        lerpTimer = 0f;
        
    }
    public void Die()
    {
        
        
    }

    public void IncreaseHealth()
    {
        
        maxHealth += maxHealth * 0.1f;
        health += maxHealth * 0.1f;
    }
    
}
