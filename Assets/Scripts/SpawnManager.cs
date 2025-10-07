using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{    
    [SerializeField] private string chickenPoolTag = "Chicken";
    [SerializeField] private int maxChickens = 10;
    [SerializeField] private float spawnCheckInterval = 10f;
    [SerializeField] private IObjectPool objectPool;

    List<Transform> spawnPoints = new List<Transform>();
    private Coroutine checkCoroutine;

    private void Start()
    {
        CacheSpawnPoints();
        SpawnMultipleChickens(maxChickens);
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
        while (true)
        {
            yield return new WaitForSeconds(spawnCheckInterval);
            if (this == null || !isActiveAndEnabled) yield break; //проверку на уничтожение объекта
            int activeChickens = objectPool.GetActiveObjectsCount(chickenPoolTag);

            if (activeChickens < maxChickens)
            {
                int chickensToSpawn = maxChickens - activeChickens;
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
        if (spawnPoints.Count == 0 || objectPool == null) return;

        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject chicken = objectPool.SpawnFromPool(chickenPoolTag, spawnPos, Quaternion.identity);

        if (chicken == null)
        {
            Debug.LogWarning("Failed to spawn chicken from pool");
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        // небольшой рандом для естественности
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
