using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClosestEnemySelector : ITargetSelector
{
    public Transform SelectTarget(List<Transform> targets, Transform owner)
    {

        if (targets == null || targets.Count == 0)
            return null;

        Transform clossest = null;
        float minDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float dist = Vector3.SqrMagnitude(
                target.position - owner.position
            );

            if (dist < minDistance)
            {
                minDistance = dist;
                clossest = target;
            }
        }

        return clossest;
    }
}
