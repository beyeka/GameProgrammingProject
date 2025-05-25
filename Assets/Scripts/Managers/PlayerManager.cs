using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private MouseLooking mouseLooking;

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

        playerMovement.StartGameplay();
        playerHealth.StartGameplay();
        mouseLooking.StartGameplay();
        StartGameplay_Guns();
    }

    public void IncreaseHealth()
    {
        playerHealth.IncreaseHealth();
    }

    public void FinishGameplay()
    {
        _isActive = false;

        playerMovement.FinishGameplay();
        playerHealth.FinishGameplay();
        mouseLooking.FinishGameplay();
        FinishGameplay_Guns();
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
        GameManager.Instance.FinishGameplay(false);
    }

    private void UnsubscribeEvents()
    {
        playerHealth.PlayerDied -= OnPlayerDied;
    }

    private void StartGameplay_Guns()
    {
        foreach (var gun in guns)
        {
            gun.StartGameplay();
        }
    }

    private void FinishGameplay_Guns()
    {
        foreach (var gun in guns)
        {
            gun.FinishGameplay();
        }
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