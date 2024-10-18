using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] chickenPrefab;    
    List<Transform> points = new List<Transform>();
    private int chickenCount;    

    void Start()
    {
        Transform pointsObject = GameObject.FindGameObjectWithTag("PointsChicken").transform;
        foreach (Transform t in pointsObject)
        {
            points.Add(t);
        }
        StartCoroutine(CheckChicken());
    }   
    void SpawnChicken()
    {
        Vector3 spawnPos = (points[Random.Range(0, points.Count)].position);
        int chickenIndex = Random.Range(0, chickenPrefab.Length);
        Instantiate(chickenPrefab[chickenIndex], spawnPos, chickenPrefab[chickenIndex].transform.rotation);        
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
