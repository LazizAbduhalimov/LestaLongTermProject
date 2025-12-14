using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;
    public Action<int, int, int> OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        var oldHealth = CurrentHealth;
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(oldHealth, CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false);
    }
}