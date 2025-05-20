using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpSpawnData[] spawnConfigs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 15f;

    private void Start()
    {
        StartCoroutine(SpawnPowerUpsLoop());
    }

    private IEnumerator SpawnPowerUpsLoop()
    {
        while (true)
        {
            SpawnRandomPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomPowerUp()
    {
        if (spawnConfigs.Length == 0 || spawnPoints.Length == 0) return;

        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var config = spawnConfigs[Random.Range(0, spawnConfigs.Length)];

        var obj = Instantiate(config.powerUpPrefab, spawnPoint.position, Quaternion.identity);
        var pickup = obj.GetComponent<PowerUpPickup>();
        if (pickup != null)
        {
            pickup.SetPowerUp(config.powerUpData);
        }
    }
}