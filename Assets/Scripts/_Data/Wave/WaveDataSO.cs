// ScriptableObject for advanced enemy wave configurations, supports multiple spawn sets with custom spawn points and enemy data.

using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Spawning/Advanced Wave Data")]
public class WaveDataSO : ScriptableObject
{
    [System.Serializable]
    public class SpawnSet
    {
        public int spawnPointIndex; 
        public EnemySpawnData[] enemiesToSpawn;
    }

    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject enemyPrefab;
        public int count;
        public float spawnDelay = 0.5f;
    }

    public SpawnSet[] spawnSets;
}