using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthComponent HealthComponent;
    public Image Image;

    public void OnEnable()
    {
        HealthComponent.OnHealthChanged += UpdateHealthBar;       
    }

    public void OnDisable()
    {
        HealthComponent.OnHealthChanged -= UpdateHealthBar;
    }

    public void UpdateHealthBar(int oldHealth, int currentHealth, int maxHealth)
    {
        var healthNormalized = (float)currentHealth / maxHealth;
        Image.fillAmount = healthNormalized;
    }
}