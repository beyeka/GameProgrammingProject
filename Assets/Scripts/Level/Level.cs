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

    public void FinishGameplay()
    {
        enemySpawner.FinishGameplay();
        powerUpManager.FinishGameplay();
        playerManager.FinishGameplay();
        bossSpawner.FinishGameplay();
    }
}