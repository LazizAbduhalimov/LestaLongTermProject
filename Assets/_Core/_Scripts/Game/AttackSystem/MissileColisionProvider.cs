using UnityEngine;

public class MissileColisionProvider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            VFXPools.Instance.FireImpactPool.GetFromPool(transform.position);
            gameObject.SetActive(false);
        }
    }
}