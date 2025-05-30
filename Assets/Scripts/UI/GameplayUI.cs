// Handles in-game UI elements like health bars and ammo count. Inherits from UIPage for shared UI functionality.
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : UIPage
{
    [SerializeField] private TextMeshProUGUI ammoText;

    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    // Updates the displayed ammo count text.
    public void SetAmmoText(string text)
    {
        ammoText.SetText(text);
    }
}