// Manages player health logic, including damage, healing, death, and animated UI feedback.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    private float lerpTimer;
    public float maxHealth;
    public float chipSpeed = 2f;

    private Image frontHealthBar;
    private Image backHealthBar;
    private TextMeshProUGUI healthText;

    public event Action PlayerDied;

    private bool _isActive;

    // Runs health logic if active, updates UI, and handles debug input for damage/heal.
    public void CustomUpdate()
    {
        if (!_isActive)
            return;

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

    // Animates front/back health bars to show health changes over time and updates text.
    public void UpdateHealthUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillBack > hFraction)
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

    // Decreases health, clamps it, and triggers death if needed (ignores damage if GodMode is active).
    public void TakeDamage(float amount)
    {
        if(GameManager.IsGodModeActive)
            return;
        
        health -= amount;
        lerpTimer = 0f;
        if (health <= 0)
        {
            Die();
        }
    }

    // Heals player without exceeding max health, resets lerp timer.
    public void RestoreHealth(float healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
        lerpTimer = 0f;
    }

    // Invokes death event.
    public void Die()
    {
        PlayerDied?.Invoke();
    }

    // Increases max and current health by 10%.
    public void IncreaseHealth()
    {
        maxHealth += maxHealth * 0.1f;
        health += maxHealth * 0.1f;
    }

    // Initializes health and binds UI references from the UIManager.
    public void StartGameplay()
    {
        ResetHp();

        frontHealthBar = UIManager.Instance.gameplayUI.frontHealthBar;
        backHealthBar = UIManager.Instance.gameplayUI.backHealthBar;
        healthText = UIManager.Instance.gameplayUI.healthText;

        _isActive = true;
    }

    // Disables health updates.
    public void FinishGameplay()
    {
        _isActive = false;
    }

    // Resets current health to max.
    private void ResetHp()
    {
        health = maxHealth;
    }
}