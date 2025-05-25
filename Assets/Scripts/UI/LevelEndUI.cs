public class LevelEndUI : UIPage
{
    public CustomButton menuButton;
    
    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnMenuButtonClicked()
    {
        Hide();
        
        GameManager.Instance.EndTheGameplayCompletely();
    }
}