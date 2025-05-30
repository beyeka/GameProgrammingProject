// Handles main menu UI logic, exposing button click events for play, settings, and quit actions.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : UIPage
{
    public CustomButton playButton;
    public CustomButton settingsButton;
    public CustomButton quitButton;

    public event Action PlayButtonClicked;
    public event Action SettingsButtonClicked;
    public event Action QuitButtonClicked;

    // Subscribes all button click listeners.
    private void Awake()
    {
        SubscribeEvents();
    }

    // Connects each CustomButton's click event to the corresponding method.
    private void SubscribeEvents()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    

    // Invokes Play button event.
    private void OnPlayButtonClicked()
    {
        PlayButtonClicked?.Invoke();
    }

    // Invokes Settings button event.
    private void OnSettingsButtonClicked()
    {
        SettingsButtonClicked?.Invoke();
    }
    
    // Invokes Quit button event.
    private void OnQuitButtonClicked()
    {
        QuitButtonClicked?.Invoke();
    }
}