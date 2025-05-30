// Spawns a boss at a specific location with a delay after gameplay starts, and cleans up on gameplay end.

using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject sceneBoss;
    
    // Starts coroutine to spawn boss after a fixed delay.
    private IEnumerator SpawnBoss()
    {
        StartCoroutine(SpawnBossAfterDelay(5f));
        
        yield return null;
    }

    // Waits for a given time, then instantiates the boss at the spawn point.
    private IEnumerator SpawnBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sceneBoss=Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        
        
    }
    
    // Called to initiate boss spawn flow.
    public void StartGameplay()
    {
        StartCoroutine(SpawnBoss());
    }

    // Stops the spawn coroutine and destroys the boss if it exists.
    public void FinishGameplay()
    {
        StopCoroutine(SpawnBoss());
        
        Destroy(sceneBoss);
    }
}