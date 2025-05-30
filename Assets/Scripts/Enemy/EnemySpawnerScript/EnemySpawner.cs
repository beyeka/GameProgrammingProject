// Spawns a wave of enemies at predefined spawn points based on WaveDataSO configuration.
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveDataSO waveData;
    [SerializeField] private Transform[] spawnPoints;
    
    // Starts spawn routines for all spawn sets in the wave.
    private IEnumerator SpawnWave()
    {
        foreach (var spawnSet in waveData.spawnSets)
        {
            StartCoroutine(SpawnAtPoint(spawnSet));
        }

        yield return null;
    }

    // Spawns enemies at the specified spawn point with delay between spawns.

    private IEnumerator SpawnAtPoint(WaveDataSO.SpawnSet set)
    {
        if (set.spawnPointIndex < 0 || set.spawnPointIndex >= spawnPoints.Length)
        {
            Debug.LogError("Invalid spawn point index!");
            yield break;
        }

        Transform spawnPoint = spawnPoints[set.spawnPointIndex];

        foreach (var enemyData in set.enemiesToSpawn)
        {
            for (int i = 0; i < enemyData.count; i++)
            {
                EnemyPoolManager.Instance.Spawn(enemyData.enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(enemyData.spawnDelay);
            }
        }
    }

    // Starts the enemy wave spawning process.
    public void StartGameplay()
    {
        StartCoroutine(SpawnWave());
    }

    // Stops the enemy wave spawning process.
    public void FinishGameplay()
    {
        StopCoroutine(SpawnWave());
    }
    
}