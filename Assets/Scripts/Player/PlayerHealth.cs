using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject damageOverlay;

    [SerializeField] private Image healthFillImage;
    private Color fullHealthColor = Color.green;
    private Color mediumHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;
    private Color targetColor;

    public event System.Action OnDeath;

    public int CurrentHealth { get; private set; }    

    private void Start()
    {
        CurrentHealth = PlayerPrefs.GetInt("Health", _gameConfig.PlayerMaxHealth);
        if(CurrentHealth <= 0) { CurrentHealth = _gameConfig.PlayerMaxHealth; }
        healthSlider.maxValue = _gameConfig.PlayerMaxHealth;
        healthSlider.value = CurrentHealth;

        UpdateHealthColor();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return; // защита от отрицательного урона
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _gameConfig.PlayerMaxHealth);
        healthSlider.value = CurrentHealth;
        PlayerPrefs.SetInt("Health", CurrentHealth);
        UpdateHealthColor();

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
        UpdateHealthColor();
    }

    private void UpdateHealthColor()
    {
        float healthPercentage = (float)CurrentHealth / _gameConfig.PlayerMaxHealth;

        if (healthPercentage > 0.5f)    // Зеленый ? Желтый (100% - 50%)
        {
            float t = (healthPercentage - 0.5f) / 0.5f;
            targetColor = Color.Lerp(mediumHealthColor, fullHealthColor, t);
        }

        else   // Желтый ? Красный (50% - 0%)
        {            
            float t = healthPercentage / 0.5f;
            targetColor = Color.Lerp(lowHealthColor, mediumHealthColor, t);
        }

        if (healthFillImage != null)
        {
            healthFillImage.color = targetColor;
        }
    }

    private IEnumerator ShowDamageEffect()
    {
        damageOverlay.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageOverlay.SetActive(false);
    }
}