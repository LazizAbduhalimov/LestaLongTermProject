using UnityEngine;

public class MissileColisionProvider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponentInParent<HealthComponent>();
        if (health != null)
        {
            health.TakeDamage(2);
        }
        Boom();
    }

    private void Boom()
    {
        VFXPools.Instance.FireImpactPool.GetFromPool(transform.position);
        gameObject.SetActive(false);
    }
}