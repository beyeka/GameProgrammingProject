using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameManager gameManager;

    public MainMenuUI mainMenuUI;
    public GameplayUI gameplayUI;
    public FailUI failUI;
    public SuccessUI successUI;
    public SettingsUI settingsUI;

    private void Awake()
    {
        Instance = this;

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        gameManager.GameStateChanged += OnGameStateChanged;
        gameManager.GameplayFinished += OnGameplayFinished;

        mainMenuUI.PlayButtonClicked += OnPlayButtonClicked;
        mainMenuUI.SettingsButtonClicked += OnSettingsButtonClicked;
        mainMenuUI.QuitButtonClicked += OnQuitButtonClicked;
    }

    private void OnGameplayFinished(bool isSuccess)
    {
        if (isSuccess)
        {
            successUI.Show();
        }
        else
        {
            failUI.Show();
        }
    }

    private void OnGameStateChanged(GameState oldGameState, GameState newGameState)
    {
        HideAllUI();

        switch (newGameState)
        {
            case GameState.MainMenu:
                mainMenuUI.Show();
                break;
            case GameState.Gameplay:
                gameplayUI.Show();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
    }

    private void OnPlayButtonClicked()
    {
        var levelIndex = 0;
        gameManager.StartGameplay(levelIndex);
    }

    private void OnSettingsButtonClicked()
    {
        settingsUI.Show();
    }

    private void OnQuitButtonClicked()
    {
        gameManager.Quit();
    }

    private void HideAllUI()
    {
        mainMenuUI.Hide();
        gameplayUI.Hide();
        failUI.Hide();
        successUI.Hide();
        settingsUI.Hide();
    }

    private void UnsubscribeEvents()
    {
        gameManager.GameStateChanged -= OnGameStateChanged;

        mainMenuUI.PlayButtonClicked -= OnPlayButtonClicked;
        mainMenuUI.SettingsButtonClicked -= OnSettingsButtonClicked;
        mainMenuUI.QuitButtonClicked -= OnQuitButtonClicked;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();

        Instance = null;
    }
}