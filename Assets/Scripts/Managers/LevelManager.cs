using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
   public LevelSystem levelSystem; 

    public Level CurrentLevel { private set; get; }

    public void Initialize()
    {
    }

    public void StartGameplay(int levelIndex)
    {
        ClearCurrentLevel();
        
        CurrentLevel = CreateLevel(levelIndex);
        CurrentLevel.StartGameplay();
        
        levelSystem.ResetEverything();
        levelSystem.StartGameplay();
    }

    public void FinishGameplay()
    {
        CurrentLevel.FinishGameplay();
        levelSystem.FinishGameplay();
    }

    private void ClearCurrentLevel()
    {
        if (CurrentLevel)
        {
            Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
        }
    }

    private Level CreateLevel(int levelIndex)
    {
        var levelPrefab = levels[levelIndex % levels.Count];
        return Instantiate(levelPrefab, transform);
    }

    public void EndTheGameplayCompletely()
    {
        ClearCurrentLevel();
    }
}