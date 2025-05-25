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

    public void SetAmmoText(string text)
    {
        ammoText.SetText(text);
    }
}