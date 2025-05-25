using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public PlayerManager playerManager; 
    public EnemySpawner enemySpawner; 
    public PowerUpManager powerUpManager;
    
    public void StartGameplay()
    {
        enemySpawner.StartGameplay();
        powerUpManager.StartGameplay();
        playerManager.StartGameplay();
    }

    public void FinishGameplay()
    {
        enemySpawner.FinishGameplay();
        powerUpManager.FinishGameplay();
        playerManager.FinishGameplay();
    }
}
