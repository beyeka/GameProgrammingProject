// Base class for level end UIs (like win/lose screens). Handles return to main menu via a custom button.
public class LevelEndUI : UIPage
{
    public CustomButton menuButton;
    
    // Subscribes the menu button to the OnMenuButtonClicked handler.
    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    // Hides the UI and triggers GameManager to end gameplay and return to main menu.
    private void OnMenuButtonClicked()
    {
        Hide();
        
        GameManager.Instance.EndTheGameplayCompletely();
    }
}