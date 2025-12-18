using UnityEngine;

public class ExplosionAfterDeath : MonoBehaviour
{
    public HealthComponent HealthComponent;

    private void OnValidate() 
    {
        if (HealthComponent == null)
        {
            HealthComponent = GetComponent<HealthComponent>();
        }
    }

    private void OnEnable()
    {
        HealthComponent.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int oldHealth, int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        VFXPools.Instance.ExplosionImpactPool.GetFromPool(transform.position);
    }
}