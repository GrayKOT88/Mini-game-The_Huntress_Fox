using System.Collections;
using UnityEngine;

public class ExplosionScript : MonoBehaviour, IPooledObject
{
    private ObjectPool objectPool;
    private string poolTag = "Explosion";

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
        StartCoroutine(ReturnToPool());
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(2);
        if (objectPool != null)
            objectPool.ReturnToPool(poolTag, gameObject);
    }
}
