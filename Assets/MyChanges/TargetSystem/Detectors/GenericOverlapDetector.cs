using System.Collections.Generic;
using UnityEngine;

public class GenericOverlapDetector<T> : ITargetDetector where T : Component
{
    private float radius;

    public GenericOverlapDetector(float radius)
    {
        this.radius = radius;
    }

    public List<Transform> DetectTargets(Transform owner)
    {
        var hits = Physics.OverlapSphere(owner.position, radius);
        var result = new List<Transform>();

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<T>(out _))
                result.Add(hit.transform);
        }

        return result;
    }
}
