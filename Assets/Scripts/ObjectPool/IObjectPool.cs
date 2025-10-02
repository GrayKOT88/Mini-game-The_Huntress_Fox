using UnityEngine;

public interface IObjectPool
{
    GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation);
    void ReturnToPool(string tag, GameObject objectToReturn);
    int GetActiveObjectsCount(string tag);
}
