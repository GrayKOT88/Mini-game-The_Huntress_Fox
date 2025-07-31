using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 15;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject damageOverlay;

    public event System.Action OnDeath;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    private void Start()
    {
        CurrentHealth = PlayerPrefs.GetInt("Health", maxHealth);
        if(CurrentHealth <= 0) { CurrentHealth = maxHealth; }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
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
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
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