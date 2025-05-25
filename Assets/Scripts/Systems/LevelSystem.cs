using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXp;
    public float requiredXp;

    private float lerpTimer;
    private float delayTImer;

    [Header("UI")] public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    private bool _isActive;

    public void StartGameplay()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        levelText.text = "Level " + level;

        _isActive = true;
    }

    void Update()
    {
        if (!_isActive)
            return;

        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            GainExperienceFlatRate(20);
        }

        if (currentXp > requiredXp)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float frontXpB = frontXpBar.fillAmount;
        if (frontXpB < xpFraction)
        {
            delayTImer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTImer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(frontXpB, backXpBar.fillAmount, percentComplete);
            }
        }

        xpText.text = currentXp + "/" + requiredXp;
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
        lerpTimer = 0f;
        delayTImer = 0f;
    }

    public void LevelUp()
    {
        level++;
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);

        GameManager.Instance.gameplayController.IncreaseHealth();

        levelText.text = "Level " + level;
        requiredXp += level * 6;
    }

    public void ResetEverything()
    {
        level = 1;
        currentXp = 0;
        requiredXp = 100;
        lerpTimer = 0;
        delayTImer = 0;
    }

    public void FinishGameplay()
    {
        _isActive = false;
    }
}