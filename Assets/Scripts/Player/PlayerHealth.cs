using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject damageOverlay;
    public event System.Action OnDeath;

    public int CurrentHealth { get; private set; }    

    private void Start()
    {
        CurrentHealth = PlayerPrefs.GetInt("Health", _gameConfig.PlayerMaxHealth);
        if(CurrentHealth <= 0) { CurrentHealth = _gameConfig.PlayerMaxHealth; }
        healthSlider.maxValue = _gameConfig.PlayerMaxHealth;
        healthSlider.value = CurrentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return; // защита от отрицательного урона
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _gameConfig.PlayerMaxHealth);
        healthSlider.value = CurrentHealth;
        PlayerPrefs.SetInt("Health", CurrentHealth);

        if (damage > 0)
        {
            StartCoroutine(ShowDamageEffect());
        }

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, _gameConfig.PlayerMaxHealth);
        healthSlider.value = CurrentHealth;
        PlayerPrefs.SetInt("Health", CurrentHealth);
    }

    private IEnumerator ShowDamageEffect()
    {
        damageOverlay.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageOverlay.SetActive(false);
    }
}