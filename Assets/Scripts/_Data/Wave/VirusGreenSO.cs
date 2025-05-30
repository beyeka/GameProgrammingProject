// ScriptableObject (derived from WaveDataSO) for defining a wave of green virus enemies, including spawn settings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusGreenSO : WaveDataSO
{
    [System.Serializable]
    public class VirusGreenSOEnemySpawnData
    {
        public GameObject enemyPrefab;
        public int count;
        public float spawnDelay = 0.5f;
    }

    public VirusGreenSOEnemySpawnData[] enemiesInWave;
}
