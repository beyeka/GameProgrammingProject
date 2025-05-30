// Central controller for managing level-wide systems and coordinating their start/finish lifecycle.

using System.Collections;
using System.Collections.Generic;
using DigitalRuby.RainMaker;
using UnityEngine;

public class Level : MonoBehaviour
{
    public PlayerManager playerManager;
    public EnemySpawner enemySpawner;
    public PowerUpManager powerUpManager;
    public RainScript rainScript;
    public BossSpawner bossSpawner;
    
    // Starts all core gameplay systems and toggles rain sound based on music settings.
    public void StartGameplay()
    {
        enemySpawner.StartGameplay();
        bossSpawner.StartGameplay();
        powerUpManager.StartGameplay();
        playerManager.StartGameplay();
        
        var isMusicOn = SoundManager.Instance.isMusicOn;

        if (isMusicOn)
            rainScript.UnmuteAll();
        else
            rainScript.MuteAll();
    }

    // Stops all core gameplay systems.
    public void FinishGameplay()
    {
        enemySpawner.FinishGameplay();
        powerUpManager.FinishGameplay();
        playerManager.FinishGameplay();
        bossSpawner.FinishGameplay();
    }
}
