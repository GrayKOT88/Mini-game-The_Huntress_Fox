using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
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
                _health.Heal(_gameConfig.HealthFromChicken);
            }
            _score.AddScore(_gameConfig.ScorePerChicken);
        }
        else if (other.CompareTag("Dog"))
        {
            _health.TakeDamage(_gameConfig.DamageFromDog);
        }
    }
}