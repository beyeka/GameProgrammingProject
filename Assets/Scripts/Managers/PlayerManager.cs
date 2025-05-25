using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private MouseLooking mouseLooking;
    [SerializeField] private WeaponSwitching weaponSwitching;

    [SerializeField] private List<Gun> guns;

    private bool _isActive;

    private void SubscribeEvents()
    {
        playerHealth.PlayerDied += OnPlayerDied;
    }

    private void Awake()
    {
        SubscribeEvents();
    }

    public void StartGameplay()
    {
        _isActive = true;

        weaponSwitching.StartGameplay();
        playerMovement.StartGameplay();
        playerHealth.StartGameplay();
        mouseLooking.StartGameplay();
    }

    public void IncreaseHealth()
    {
        playerHealth.IncreaseHealth();
    }

    public void FinishGameplay()
    {
        _isActive = false;

        weaponSwitching.FinishGameplay();
        playerMovement.FinishGameplay();
        playerHealth.FinishGameplay();
        mouseLooking.FinishGameplay();
    }

    private void Update()
    {
        if (!_isActive)
            return;

        playerMovement.CustomUpdate();
        playerHealth.CustomUpdate();
        mouseLooking.CustomUpdate();
        CustomUpdateGuns();
    }

    private void OnPlayerDied()
    {
        SoundManager.Instance.PlaySound(SFXKeys.PlayerDeath);
        GameManager.Instance.FinishGameplay(false);
    }

    private void UnsubscribeEvents()
    {
        playerHealth.PlayerDied -= OnPlayerDied;
    }
    
    private void CustomUpdateGuns()
    {
        foreach (var gun in guns)
        {
            gun.CustomUpdate();
        }
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}