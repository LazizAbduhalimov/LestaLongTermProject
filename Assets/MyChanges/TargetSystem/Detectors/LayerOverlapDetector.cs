using System.Collections.Generic;
using UnityEngine;

public class LayerOverlapDetector : ITargetDetector
{
    private float radius;
    private LayerMask mask;

    public LayerOverlapDetector(float radius, LayerMask mask)
    {
        this.radius = radius;
        this.mask = mask;
    }

    public List<Transform> DetectTargets(Transform owner)
    {
        var hits = Physics.OverlapSphere(owner.position, radius, mask);
        var result = new List<Transform>();

        foreach (var hit in hits)
            result.Add(hit.transform);

        return result;
    }
}
