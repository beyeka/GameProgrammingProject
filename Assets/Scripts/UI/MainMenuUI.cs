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

    private void Awake()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnQuitButtonClicked()
    {
        QuitButtonClicked?.Invoke();
    }

    private void OnPlayButtonClicked()
    {
        PlayButtonClicked?.Invoke();
    }

    private void OnSettingsButtonClicked()
    {
        SettingsButtonClicked?.Invoke();
    }
}