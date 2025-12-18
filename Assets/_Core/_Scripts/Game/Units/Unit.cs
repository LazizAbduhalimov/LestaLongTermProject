using UnityEngine;

public class Unit : MonoBehaviour
{
    public HealthComponent HealthComponent;

    private void OnValidate()
    {
        HealthComponent ??= GetComponent<HealthComponent>();
    }
}
