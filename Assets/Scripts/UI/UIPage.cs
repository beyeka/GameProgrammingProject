using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPage : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        UpdateView();
    }

    protected virtual void UpdateView()
    {
    }
}