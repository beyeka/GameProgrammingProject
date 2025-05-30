// Singleton GameManager controlling game state, scene-level systems, and gameplay flow (start, finish, quit, etc.)
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

     
    [SerializeField] private SoundManager soundManager;
    public GameplayController gameplayController;

    public GameState GameState { private set; get; }

    public static bool IsGodModeActive;
    public event Action<GameState, GameState> GameStateChanged;
    public event Action<bool> GameplayFinished;


    // Initializes managers, plays menu music, sets initial game state.
    private void Start()
    {
        Instance = this;

        InitializeManagers();

        SoundManager.Instance.PlayMainMenuMusic();
        
        ChangeGameState(GameState.MainMenu);
    }

    // Temporary debug shortcut to finish gameplay with 'K' key.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FinishGameplay(true);
        }
    }

    // Calls initialization methods on sound and gameplay controllers.
    private void InitializeManagers()
    {
        soundManager.Initialize();
        gameplayController.Initialize();
    }

    // Starts gameplay for a given level, changes state, sets cursor mode, and starts music.
    public void StartGameplay(int levelIndex)
    {
        gameplayController.StartGameplay(levelIndex);

        ChangeGameState(GameState.Gameplay);
        SetCursorMode(true);
        
        SoundManager.Instance.PlayGameplayMusic();
    }

    // Freezes game, sets cursor for UI, stops music, finishes gameplay controller, triggers event.
    public void FinishGameplay(bool isSuccess)
    {
        SetTimeScale(0);
        
        SetCursorMode(false);

        gameplayController.FinishGameplay(isSuccess);
        
        SoundManager.Instance.StopGameplayMusic();

        GameplayFinished?.Invoke(isSuccess);
    }

    // Ends gameplay, resets timescale, changes state back to main menu, restarts music.
    public void EndTheGameplayCompletely()
    {
        SoundManager.Instance.PlayMainMenuMusic();
        
        gameplayController.EndTheGameplayCompletely();

        SetTimeScale(1);

        ChangeGameState(GameState.MainMenu);
    }

    // Updates game state and notifies subscribers via event.
    private void ChangeGameState(GameState newGameState)
    {
        var oldGameState = GameState;

        GameState = newGameState;

        GameStateChanged?.Invoke(oldGameState, newGameState);
    }

    // Quits the application (build only).
    public void Quit()
    {
        Debug.Log("Quiting Gameplay");
        Application.Quit();
    }

    // Sets Unity's time scale (pause/resume effect)
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    // Locks/hides or unlocks/shows the mouse cursor depending on gameplay mode.    
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

    // Cleans up static reference to the singleton.
    private void OnDestroy()
    {
        Instance = null;
    }
}

// Defines global game states.
public enum GameState
{
    MainMenu,
    Gameplay
}