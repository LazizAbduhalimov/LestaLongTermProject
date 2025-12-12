using UnityEngine;

public class MissileColisionProvider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            gameObject.SetActive(false);
        }
    }
}