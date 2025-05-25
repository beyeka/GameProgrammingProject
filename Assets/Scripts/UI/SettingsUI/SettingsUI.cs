using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UIPage
{
    [SerializeField] private CustomButton closeButton;
    [SerializeField] private CustomButton toggleButton;

    [SerializeField] private GameObject checkObject;
    [SerializeField] private CustomButton sfxButton;
    [SerializeField] private CustomButton musicButton;

    [SerializeField] private Image sfxOffIcon;
    [SerializeField] private Image musicOffIcon;

    private void Awake()
    {
        toggleButton.onClick.AddListener(OnToggleButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        sfxButton.onClick.AddListener(OnSFXButtonClicked);
        musicButton.onClick.AddListener(OnMusicButtonClicked);
    }

    private void OnMusicButtonClicked()
    {
        SoundManager.Instance.isMusicOn = !SoundManager.Instance.isMusicOn;
        SoundManager.Instance.MusicOnOffChanged();
        UpdateView();
    }

    private void OnSFXButtonClicked()
    {
        SoundManager.Instance.isSfxOn = !SoundManager.Instance.isSfxOn;
        UpdateView();
    }

    private void OnCloseButtonClicked()
    {
        Hide();
    }

    protected override void UpdateView()
    {
        base.UpdateView();

        checkObject.gameObject.SetActive(GameManager.IsGodModeActive);

        musicOffIcon.enabled = !SoundManager.Instance.isMusicOn;
        sfxOffIcon.enabled = !SoundManager.Instance.isSfxOn;
    }

    private void OnToggleButtonClicked()
    {
        GameManager.IsGodModeActive = !GameManager.IsGodModeActive;

        UpdateView();
    }
}