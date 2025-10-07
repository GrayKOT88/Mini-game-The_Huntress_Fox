using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerHealth _health;
    private PlayerScore _score;
    private PlayerAudio _audio;
    private int _damageFromDog = 5;
    private int _healthFromChicken = 1;
    private int _scorePoint = 1;

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
                _health.Heal(_healthFromChicken);
            }
            _score.AddScore(_scorePoint);
        }
        else if (other.CompareTag("Dog"))
        {
            _health.TakeDamage(_damageFromDog);
        }
    }
}