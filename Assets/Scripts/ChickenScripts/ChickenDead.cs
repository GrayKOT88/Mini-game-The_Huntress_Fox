using UnityEngine;

public class ChickenDead : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticle;    
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            Vector3 partPos = new Vector3(transform.position.x, 0.2f, transform.position.z);            
            Destroy(gameObject);
            Instantiate(explosionParticle, partPos, explosionParticle.transform.rotation);            
        }
    }
}
