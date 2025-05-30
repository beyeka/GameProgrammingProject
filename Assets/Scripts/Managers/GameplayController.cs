// Central gameplay controller that acts as a bridge between systems like LevelManager, XP handling, health, and VFX.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] public LevelManager levelManager;

    [SerializeField] private ParticleSystem deadPS;

    private List<ParticleSystem> _deadPSList;

    // Initializes the level manager and sets up lists.
    public void Initialize()
    {
        _deadPSList = new List<ParticleSystem>();

        levelManager.Initialize();
    }

    // Instantiates and plays a particle system at the given position when an enemy dies.
    public void PlayEnemyDeadPS(Vector3 position)
    {
        var newPs = Instantiate(deadPS, transform);
        newPs.transform.position = position;
        newPs.Play();
    }

    // Adds experience to the player through LevelSystem.
    public void GiveExp(float expValue)
    {
        levelManager.levelSystem.GainExperienceFlatRate(expValue);
    }

    // Calls player's IncreaseHealth method from the current level.
    public void IncreaseHealth()
    {
        var playerManager = levelManager.CurrentLevel.playerManager;
        playerManager.IncreaseHealth();
    }

    // Starts gameplay on the selected level via LevelManager.
    public void StartGameplay(int levelIndex)
    {
        levelManager.StartGameplay(levelIndex);
    }

    // Finishes gameplay (win/lose), called from player death or wave clear.
    public void FinishGameplay(bool isSuccess)
    {
        levelManager.FinishGameplay();
    }

    // Fully ends gameplay session and resets level state.
    public void EndTheGameplayCompletely()
    {
        levelManager.EndTheGameplayCompletely();
    }
}