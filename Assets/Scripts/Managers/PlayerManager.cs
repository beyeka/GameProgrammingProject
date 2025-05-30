// Handles coordination between player systems: movement, health, input, weapons. Manages lifecycle and updates.
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

    // Subscribes to health-related events like player death.
    private void SubscribeEvents()
    {
        playerHealth.PlayerDied += OnPlayerDied;
    }

    // Initializes event subscriptions.
    private void Awake()    
    {
        SubscribeEvents();
    }

    // Activates and starts all player subsystems.
    public void StartGameplay()
    {
        _isActive = true;

        weaponSwitching.StartGameplay();
        playerMovement.StartGameplay();
        playerHealth.StartGameplay();
        mouseLooking.StartGameplay();
    }
    
    // Calls health boost on PlayerHealth.
    public void IncreaseHealth()
    {
        playerHealth.IncreaseHealth();
    }

    // Stops all player systems and disables player input.
    public void FinishGameplay()
    {
        _isActive = false;

        weaponSwitching.FinishGameplay();
        playerMovement.FinishGameplay();
        playerHealth.FinishGameplay();
        mouseLooking.FinishGameplay();
    }

    // Runs per-frame updates for player systems if gameplay is active.
    private void Update()
    {
        if (!_isActive)
            return;

        playerMovement.CustomUpdate();
        playerHealth.CustomUpdate();
        mouseLooking.CustomUpdate();
        CustomUpdateGuns();
    }

    // Triggered on death: plays sound and finishes gameplay with failure.
    private void OnPlayerDied()
    {
        SoundManager.Instance.PlaySound(SFXKeys.PlayerDeath);
        GameManager.Instance.FinishGameplay(false);
    }

    // Cleans up event subscriptions.
    private void UnsubscribeEvents()
    {
        playerHealth.PlayerDied -= OnPlayerDied;
    }
    
    // Updates all equipped guns each frame.
    private void CustomUpdateGuns()
    {
        foreach (var gun in guns)
        {
            gun.CustomUpdate();
        }
    }

    // Ensures event unsubscription when destroyed.
    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}