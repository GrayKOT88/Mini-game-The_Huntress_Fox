using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{    
    [SerializeField] private string chickenPoolTag = "Chicken";
    [SerializeField] private ObjectPool objectPool;

    List<Transform> points = new List<Transform>();
    private int chickenCount;    

    private void Start()
    {
        Transform pointsObject = GameObject.FindGameObjectWithTag("PointsChicken").transform;
        foreach (Transform t in pointsObject)
        {
            points.Add(t);
        }
        StartCoroutine(CheckChicken());
    }
    
    private void SpawnChicken()
    {
        if (points.Count == 0 || objectPool == null) return;
        Vector3 spawnPos = points[Random.Range(0, points.Count)].position;
        objectPool.SpawnFromPool(chickenPoolTag, spawnPos, Quaternion.identity);        
    }

    IEnumerator CheckChicken() 
    {
        yield return new WaitForSeconds(5);
        chickenCount = GameObject.FindGameObjectsWithTag("Chicken").Length;
        if (chickenCount < 10)
        {
            SpawnChicken();
        }
        StartCoroutine(CheckChicken());
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene(0);
    }
}
