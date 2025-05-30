// Serializable class that defines which power-up data to use and the corresponding prefab to spawn in the world.
using UnityEngine;

[System.Serializable]
public class PowerUpSpawnData
{
    public PowerUpBaseSO powerUpData;
    public GameObject powerUpPrefab;
}