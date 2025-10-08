using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private PoolConfig _poolConfig;    
    [SerializeField] private ObjectPool _objectPool;    

    List<Transform> spawnPoints = new List<Transform>();
    private Coroutine checkCoroutine;

    private void Start()
    {
        CacheSpawnPoints();
        SpawnMultipleChickens(_poolConfig.MaxChickens);
        checkCoroutine = StartCoroutine(ChickenCheckRoutine());
    }

    private void CacheSpawnPoints()
    {
        Transform pointsObject = GameObject.FindGameObjectWithTag("PointsChicken").transform;
        foreach (Transform t in pointsObject)
        {
            spawnPoints.Add(t);
        }
    }
    private IEnumerator ChickenCheckRoutine()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(_poolConfig.SpawnCheckInterval);
            if (this == null || !isActiveAndEnabled || _objectPool == null)
                yield break; //проверку на уничтожение объекта
            int activeChickens = _objectPool.GetActiveObjectsCount(_poolConfig.ChickenPoolTag);

            if (activeChickens < _poolConfig.MaxChickens)
            {
                int chickensToSpawn = _poolConfig.MaxChickens - activeChickens;
                SpawnMultipleChickens(chickensToSpawn);
            }
        }        
    }

    private void SpawnMultipleChickens(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (spawnPoints.Count > 0)
            {
                SpawnChicken();
            }
        }
    }

    private void SpawnChicken()
    {
        if (spawnPoints.Count == 0 || _objectPool == null) return;

        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject chicken = _objectPool.SpawnFromPool(_poolConfig.ChickenPoolTag, spawnPos, Quaternion.identity);

        if (chicken == null)
        {
            Debug.LogWarning("Failed to spawn chicken from pool");
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];        
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f),0,Random.Range(-2f, 2f));
        return spawnPoint.position + randomOffset;
    }

    private void OnDestroy()
    {
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
        }
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene(0);
    }
}
