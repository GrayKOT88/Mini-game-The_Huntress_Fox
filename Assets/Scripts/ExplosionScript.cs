using System.Collections;
using UnityEngine;

public class ExplosionScript : MonoBehaviour, IPooledObject
{
    private ObjectPool objectPool;
    private string poolTag = "Explosion";

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());        
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(2);
        if (objectPool != null)
            objectPool.ReturnToPool(poolTag, gameObject);
        else
            gameObject.SetActive(false); // Fallback
    }
}
