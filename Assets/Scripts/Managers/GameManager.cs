using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // [SerializeField] private PLayerma FieldName; 
    [SerializeField] private SoundManager soundManager;
    public GameplayController gameplayController;

    public GameState GameState { private set; get; }

    public static bool IsGodModeActive;
    public event Action<GameState, GameState> GameStateChanged;
    public event Action<bool> GameplayFinished;


    private void Start()
    {
        Instance = this;

        InitializeManagers();

        SoundManager.Instance.PlayMainMenuMusic();
        
        ChangeGameState(GameState.MainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FinishGameplay(true);
        }
    }

    private void InitializeManagers()
    {
        soundManager.Initialize();
        gameplayController.Initialize();
    }

    public void StartGameplay(int levelIndex)
    {
        gameplayController.StartGameplay(levelIndex);

        ChangeGameState(GameState.Gameplay);
        SetCursorMode(true);
        
        SoundManager.Instance.PlayGameplayMusic();
    }

    // From success and fail popup
    public void FinishGameplay(bool isSuccess)
    {
        SetTimeScale(0);
        
        SetCursorMode(false);

        gameplayController.FinishGameplay(isSuccess);
        
        SoundManager.Instance.StopGameplayMusic();

        GameplayFinished?.Invoke(isSuccess);
    }

    public void EndTheGameplayCompletely()
    {
        SoundManager.Instance.PlayMainMenuMusic();
        
        gameplayController.EndTheGameplayCompletely();

        SetTimeScale(1);

        ChangeGameState(GameState.MainMenu);
    }

    private void ChangeGameState(GameState newGameState)
    {
        var oldGameState = GameState;

        GameState = newGameState;

        GameStateChanged?.Invoke(oldGameState, newGameState);
    }

    public void Quit()
    {
        Debug.Log("Quiting Gameplay");
        Application.Quit();
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void SetCursorMode(bool isGameplayFocussed)
    {
        if (isGameplayFocussed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}

public enum GameState
{
    MainMenu,
    Gameplay
}