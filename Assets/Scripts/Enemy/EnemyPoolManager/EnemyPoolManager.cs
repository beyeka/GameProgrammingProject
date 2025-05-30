// Manages object pooling for enemies to optimize performance by reusing GameObjects instead of instantiating/destroying.
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [System.Serializable]
    // Serializable struct defining each prefab type and how many to pre-instantiate.
    public class Pool
    {
        public GameObject prefab;
        public int initialSize = 10;
    }

    public static EnemyPoolManager Instance;

    [SerializeField] private Pool[] pools;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();

    
    // Initializes the singleton instance and preloads pooled enemies based on defined Pool configs.
    private void Awake()
    {
        Instance = this;
        foreach (var pool in pools)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            poolDictionary[pool.prefab] = queue;
        }
    }

    // Spawns an enemy from the pool or instantiates a new one if pool is empty. Sets position, rotation, and activates it.
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var queue = poolDictionary[prefab];
        GameObject obj = (queue.Count > 0) ? queue.Dequeue() : Instantiate(prefab, transform);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        obj.GetComponent<IPoolable>()?.OnSpawn();

        return obj;
    }

    // Despawns an enemy by deactivating it and returning it to the pool. Destroys it if the pool doesn't exist.
    public void Despawn(GameObject obj, GameObject prefab)
    {
        obj.GetComponent<IPoolable>()?.OnDespawn();
        obj.SetActive(false);

        if (poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab].Enqueue(obj);
        }
        else
        {
            Destroy(obj); // fallback
        }
    }
}