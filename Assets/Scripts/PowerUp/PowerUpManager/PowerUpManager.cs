using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the spawning of power-ups throughout the game at specified intervals and locations.
/// Handles random selection of power-up types and spawn positions based on configured data.
/// </summary>
public class PowerUpManager : MonoBehaviour
{
    #region Serialized Fields

    /// <summary>
    /// Array of power-up spawn configurations containing prefabs and their associated data.
    /// Each configuration defines a different type of power-up that can be spawned.
    /// </summary>
    [SerializeField] private PowerUpSpawnData[] spawnConfigs;

    /// <summary>
    /// Array of Transform points where power-ups can be spawned in the game world.
    /// Power-ups will randomly appear at one of these locations.
    /// </summary>
    [SerializeField] private Transform[] spawnPoints;

    /// <summary>
    /// Time interval between consecutive power-up spawns in seconds.
    /// Default value is 15 seconds.
    /// </summary>
    [SerializeField] private float spawnInterval = 15f;

    #endregion


    /// <summary>
    /// Coroutine that continuously spawns power-ups at the defined interval.
    /// Runs for the lifetime of the object.
    /// </summary>
    /// <returns>IEnumerator for coroutine functionality</returns>
    private IEnumerator SpawnPowerUpsLoop()
    {
        while (true)
        {
            SpawnRandomPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Spawns a random power-up at a random spawn point.
    /// Selects from available spawn configurations and locations.
    /// Sets up the power-up with appropriate data after instantiation.
    /// </summary>
    private void SpawnRandomPowerUp()
    {
        // Safety check to prevent errors if configurations or spawn points are missing
        if (spawnConfigs.Length == 0 || spawnPoints.Length == 0) return;

        // Randomly select a spawn point and power-up configuration
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var config = spawnConfigs[Random.Range(0, spawnConfigs.Length)];

        // Instantiate the power-up object at the selected position
        var obj = Instantiate(config.powerUpPrefab, spawnPoint.position, Quaternion.identity, transform);

        // Get the PowerUpPickup component and initialize it with the appropriate data
        var pickup = obj.GetComponent<PowerUpPickup>();
        if (pickup != null)
        {
            pickup.SetPowerUp(config.powerUpData);
        }
    }

    public void StartGameplay()
    {
        StartCoroutine(SpawnPowerUpsLoop());
    }

    public void FinishGameplay()
    {
        StopCoroutine(SpawnPowerUpsLoop());
    }
}