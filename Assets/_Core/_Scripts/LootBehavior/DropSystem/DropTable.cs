using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public List<DropItem> drops = new();

    private void Start()
    {
        gameObject.TryGetComponent(out HealthComponent healthComponent);
        healthComponent.OnDeath += OnDeath;
    }
    private void OnDeath()
    {

        DropSystem.Instance.Drop(this, transform.position);

    }
    

}
