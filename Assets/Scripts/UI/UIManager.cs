// Central UI manager controlling which UI screen is active based on game state and events.
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

    // Sets up singleton instance and subscribes to UI and GameManager events.
    private void Awake()
    {
        Instance = this;

        SubscribeEvents();
    }

    // Hooks into GameManager and MainMenuUI events to react to state changes and button presses.
    private void SubscribeEvents()
    {
        gameManager.GameStateChanged += OnGameStateChanged;
        gameManager.GameplayFinished += OnGameplayFinished;

        mainMenuUI.PlayButtonClicked += OnPlayButtonClicked;
        mainMenuUI.SettingsButtonClicked += OnSettingsButtonClicked;
        mainMenuUI.QuitButtonClicked += OnQuitButtonClicked;
    }

    // Displays the success or fail UI depending on gameplay result.
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

    // Shows the appropriate UI based on the new game state.
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

    // Starts the game from level 0 when Play is pressed.
    private void OnPlayButtonClicked()
    {
        var levelIndex = 0;
        gameManager.StartGameplay(levelIndex);
    }

    // Opens the Settings UI.
    private void OnSettingsButtonClicked()
    {
        settingsUI.Show();
    }

    // Calls GameManager to quit the application.
    private void OnQuitButtonClicked()
    {
        gameManager.Quit();
    }

    // Hides all UI screens to ensure only the relevant one is shown.
    private void HideAllUI()
    {
        mainMenuUI.Hide();
        gameplayUI.Hide();
        failUI.Hide();
        successUI.Hide();
        settingsUI.Hide();
    }

    // Unsubscribes all hooked events to prevent memory leaks.
    private void UnsubscribeEvents()
    {
        gameManager.GameStateChanged -= OnGameStateChanged;

        mainMenuUI.PlayButtonClicked -= OnPlayButtonClicked;
        mainMenuUI.SettingsButtonClicked -= OnSettingsButtonClicked;
        mainMenuUI.QuitButtonClicked -= OnQuitButtonClicked;
    }

    // Cleans up singleton reference and event subscriptions.
    private void OnDestroy()
    {
        UnsubscribeEvents();

        Instance = null;
    }
}