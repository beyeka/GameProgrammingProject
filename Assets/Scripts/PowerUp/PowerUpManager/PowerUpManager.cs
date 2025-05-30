// Manages the timed spawning of power-ups at random spawn points using predefined configs.
using System.Collections;
using UnityEngine;


public class PowerUpManager : MonoBehaviour
{
    
    [SerializeField] private PowerUpSpawnData[] spawnConfigs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 15f;

    
    // Continuously spawns random power-ups at intervals during gameplay.
    private IEnumerator SpawnPowerUpsLoop()
    {
        while (true)
        {
            SpawnRandomPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    // Picks a random spawn point and power-up config, instantiates it, and assigns the corresponding power-up data.
    private void SpawnRandomPowerUp()
    {
        
        if (spawnConfigs.Length == 0 || spawnPoints.Length == 0) return;

       
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var config = spawnConfigs[Random.Range(0, spawnConfigs.Length)];

        
        var obj = Instantiate(config.powerUpPrefab, spawnPoint.position, Quaternion.identity, transform);

        
        var pickup = obj.GetComponent<PowerUpPickup>();
        if (pickup != null)
        {
            pickup.SetPowerUp(config.powerUpData);
        }
    }

    // Starts the power-up spawning loop.
    public void StartGameplay()
    {
        StartCoroutine(SpawnPowerUpsLoop());
    }

    // Stops the power-up spawning loop.
    public void FinishGameplay()
    {
        StopCoroutine(SpawnPowerUpsLoop());
    }
}