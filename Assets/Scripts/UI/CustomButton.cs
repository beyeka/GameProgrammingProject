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

    protected virtual void Update()
    {
        if (Mathf.Approximately(_currentScaleFactor, _targetScaleFactor))
        {
            return;
        }

        var scaleFactor = Mathf.Lerp(_currentScaleFactor, _targetScaleFactor, scaleLerpTime * Time.deltaTime);
        UpdateScaleFactor(scaleFactor);
    }

    private void UpdateScaleFactor(float scaleFactor)
    {
        _currentScaleFactor = scaleFactor;
        transform.localScale = _originalScale * _currentScaleFactor;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData); // Make sure to call the base method to keep existing functionality
        _targetScaleFactor = 1f; // Set scale back to original when pointer exits
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        RequestClickSFX();
    }

    protected virtual void RequestClickSFX()
    {
        SoundManager.Instance.PlaySound(_clickSFX);
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();

        transition = Transition.None;
    }
#endif
}