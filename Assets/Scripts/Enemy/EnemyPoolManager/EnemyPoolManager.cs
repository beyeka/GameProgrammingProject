using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int initialSize = 10;
    }

    public static EnemyPoolManager Instance;

    [SerializeField] private Pool[] pools;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();

    private void Awake()
    {
        Instance = this;
        foreach (var pool in pools)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDictionary[pool.prefab] = queue;
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
       
        var queue = poolDictionary[prefab];
        GameObject obj = (queue.Count > 0) ? queue.Dequeue() : Instantiate(prefab);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        obj.GetComponent<IPoolable>()?.OnSpawn();

        return obj;
    }

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