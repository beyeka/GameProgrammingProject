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

    public void RestoreHealth(float healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
        lerpTimer = 0f;
    }

    public void Die()
    {
        PlayerDied?.Invoke();
    }

    public void IncreaseHealth()
    {
        maxHealth += maxHealth * 0.1f;
        health += maxHealth * 0.1f;
    }

    public void StartGameplay()
    {
        ResetHp();

        frontHealthBar = UIManager.Instance.gameplayUI.frontHealthBar;
        backHealthBar = UIManager.Instance.gameplayUI.backHealthBar;
        healthText = UIManager.Instance.gameplayUI.healthText;

        _isActive = true;
    }

    public void FinishGameplay()
    {
        _isActive = false;
    }

    private void ResetHp()
    {
        health = maxHealth;
    }
}