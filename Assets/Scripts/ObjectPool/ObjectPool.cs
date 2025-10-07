using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IObjectPool
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] prefabs;
        public int size;
        [HideInInspector] public int activeCount; // Счетчик активных объектов
    }

    [SerializeField] private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Pool> poolConfig; // доступ к конфигурации

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {        
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        poolConfig = new Dictionary<string, Pool>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            pool.activeCount = 0; // Инициализация счетчика

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefabs[Random.Range(0, pool.prefabs.Length)]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
            poolConfig.Add(pool.tag, pool); // Сохраняем конфигурацию
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count == 0) // Проверяем, есть ли доступные объекты
        {
            Debug.LogWarning($"No available objects in pool {tag}. Consider increasing pool size.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if (objectToSpawn == null)
        {
            Debug.LogError($"Null object in pool {tag}");
            return null;
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.SetObjectPool(this);
        }

        else
        {
            Debug.LogWarning($"Object from pool {tag} doesn't implement IPooledObject");
        }        

        if (poolConfig.ContainsKey(tag)) // Обновляем счетчик
        {
            poolConfig[tag].activeCount++;
        }

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (objectToReturn == null)
        {
            Debug.LogWarning("Attempted to return null object to pool");
            return;
        }

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        if (poolConfig.ContainsKey(tag) && poolConfig[tag].activeCount > 0) // Обновляем счетчик
        {
            poolConfig[tag].activeCount--;
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }

    public int GetActiveObjectsCount(string tag) // подсчет активных объектов
    {
        return poolConfig.ContainsKey(tag) ? poolConfig[tag].activeCount : 0;
    }
}
