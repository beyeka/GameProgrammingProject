// Custom UI Button that plays a click SFX and scales visually when pressed, with smooth transitions.
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    [SerializeField] private SFXKeys _clickSFX = SFXKeys.UI_Button_Click;

    private readonly Vector3 _originalScale = Vector3.one;

    public float pressScaleFactor = 0.92f;
    public float scaleLerpTime = 20f;

    private float _targetScaleFactor = 1f;
    private float _currentScaleFactor = 1f;

    
    // Overrides the button state transitions to apply scaling on press.
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (!Application.isPlaying)
        {
            return;
        }

        _targetScaleFactor = state == SelectionState.Pressed ? pressScaleFactor : 1f;

        if (instant)
        {
            UpdateScaleFactor(_targetScaleFactor);
        }
    }

    // Smoothly interpolates the button scale toward the target scale factor.
    protected virtual void Update()
    {
        if (Mathf.Approximately(_currentScaleFactor, _targetScaleFactor))
        {
            return;
        }

        var scaleFactor = Mathf.Lerp(_currentScaleFactor, _targetScaleFactor, scaleLerpTime * Time.deltaTime);
        UpdateScaleFactor(scaleFactor);
    }

    // Updates the current scale and applies it to the button's transform.
    private void UpdateScaleFactor(float scaleFactor)
    {
        _currentScaleFactor = scaleFactor;
        transform.localScale = _originalScale * _currentScaleFactor;
    }

    // Resets the scale when the pointer exits the button.
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData); 
        _targetScaleFactor = 1f; 
    }

    // Plays click SFX when the button is clicked.
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        RequestClickSFX();
    }

    // Triggers the configured sound effect via SoundManager.
    protected virtual void RequestClickSFX()
    {
        SoundManager.Instance.PlaySound(_clickSFX);
    }

    
    // Editor-only: disables default button transition for full control over visuals.
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();

        transition = Transition.None;
    }
#endif
}