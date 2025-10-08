using UnityEngine;

public class ChickenDead : MonoBehaviour, IPooledObject
{    
    private string poolTag = "Chicken";
    private ObjectPool objectPool;

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            Vector3 partPos = new Vector3(transform.position.x, 0.2f, transform.position.z);
            GameObject explosion = objectPool?.SpawnFromPool("Explosion", partPos, Quaternion.identity);            
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
            objectPool.ReturnToPool(poolTag, gameObject);        
    }
}
