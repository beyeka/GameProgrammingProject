// Manages level lifecycle: creation, start, finish, and cleanup. Also coordinates with the XP/leveling system.

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
   public LevelSystem levelSystem; 

    public Level CurrentLevel { private set; get; }

    // Placeholder for future setup if needed.
    public void Initialize()
    {
    }

    // Clears current level, instantiates new one, and starts level + level system gameplay.
    public void StartGameplay(int levelIndex)
    {
        ClearCurrentLevel();
        
        CurrentLevel = CreateLevel(levelIndex);
        CurrentLevel.StartGameplay();
        
        levelSystem.ResetEverything();
        levelSystem.StartGameplay();
    }

    // Finishes current level and stops level system.
    public void FinishGameplay()
    {
        CurrentLevel.FinishGameplay();
        levelSystem.FinishGameplay();
    }

    // Destroys the current level GameObject if it exists.
    private void ClearCurrentLevel()
    {
        if (CurrentLevel)
        {
            Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
        }
    }

    // Instantiates a level prefab from the list using modulo to wrap around.
    private Level CreateLevel(int levelIndex)
    {
        var levelPrefab = levels[levelIndex % levels.Count];
        return Instantiate(levelPrefab, transform);
    }

    // Cleans up current level when exiting gameplay.
    public void EndTheGameplayCompletely()
    {
        ClearCurrentLevel();
    }
}