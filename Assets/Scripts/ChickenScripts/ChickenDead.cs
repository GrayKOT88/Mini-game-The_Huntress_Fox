using UnityEngine;

public class ChickenDead : MonoBehaviour, IPooledObject
{
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] private string poolTag = "Chicken";
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
            Instantiate(explosionParticle, partPos, explosionParticle.transform.rotation);

            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
            objectPool.ReturnToPool(poolTag, gameObject);
        else
            FindObjectOfType<ObjectPool>()?.ReturnToPool(poolTag, gameObject);
    }
}
