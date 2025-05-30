// Base class for UI pages. Controls visibility and interaction using CanvasGroup, and provides a virtual update hook.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPage : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    // Hides the UI by setting alpha to 0 and disabling interaction.
    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // Shows the UI by enabling alpha and interaction, then calls UpdateView.
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        UpdateView();
    }

    // Optional override for updating UI elements when shown.
    protected virtual void UpdateView()
    {
    }
}