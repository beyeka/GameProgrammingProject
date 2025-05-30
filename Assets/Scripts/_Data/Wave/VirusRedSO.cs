// ScriptableObject (inheriting from WaveDataSO) for red virus enemy waves, defining spawn count and delay per enemy type.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusRedSO : WaveDataSO
{
    [System.Serializable]
    public class VirusRedSOEnemySpawnData
    {
        public GameObject enemyPrefab;
        public int count;
        public float spawnDelay = 0.5f;
    }

    public VirusRedSOEnemySpawnData[] enemiesInWave;
}
