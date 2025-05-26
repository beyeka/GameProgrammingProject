using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject sceneBoss;
    
    private IEnumerator SpawnBoss()
    {
        StartCoroutine(SpawnBossAfterDelay(5f));
        
        yield return null;
    }

    private IEnumerator SpawnBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        sceneBoss=Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        
        
    }
    
    public void StartGameplay()
    {
        StartCoroutine(SpawnBoss());
    }

    public void FinishGameplay()
    {
        StopCoroutine(SpawnBoss());
        
        Destroy(sceneBoss);
    }
}