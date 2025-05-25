using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] public LevelManager levelManager;

    [SerializeField] private ParticleSystem deadPS;

    private List<ParticleSystem> _deadPSList;

    public void Initialize()
    {
        _deadPSList = new List<ParticleSystem>();

        levelManager.Initialize();
    }

    public void PlayEnemyDeadPS(Vector3 position)
    {
        var newPs = Instantiate(deadPS, transform);
        newPs.transform.position = position;
        newPs.Play();
    }

    public void GiveExp(float expValue)
    {
        levelManager.levelSystem.GainExperienceFlatRate(expValue);
    }

    public void IncreaseHealth()
    {
        var playerManager = levelManager.CurrentLevel.playerManager;
        playerManager.IncreaseHealth();
    }

    public void StartGameplay(int levelIndex)
    {
        levelManager.StartGameplay(levelIndex);
    }

    /// <summary>
    /// Will be called by player when he dies, or enemy spawner, when player clears every wave 
    /// </summary>
    public void FinishGameplay(bool isSuccess)
    {
        levelManager.FinishGameplay();
    }

    public void EndTheGameplayCompletely()
    {
        levelManager.EndTheGameplayCompletely();
    }
}