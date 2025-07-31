using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerHealth _health;
    private PlayerScore _score;
    private PlayerAudio _audio;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _score = GetComponent<PlayerScore>();
        _audio = GetComponent<PlayerAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            _audio.PlayChickenSound();
            if (_health.CurrentHealth < _health.MaxHealth)
            {
                _health.Heal(1);
            }
            _score.AddScore(1);
        }
        else if (other.CompareTag("Dog"))
        {
            _health.TakeDamage(5);
        }
    }
}