using System.Collections;
using System.Collections.Generic;
using Modules.Coroutines;
using UnityEngine;

public class LightningAttack : AttackTypeBase
{
    public LineRenderer LightningLine;
    public float LightningDuration = 0.2f;
    public float ChainRadius = 10f;
    public float ChainInterval = 0.2f;
    public int MaxBounces = 3;
    public LayerMask EnemyMask;
    public float DamagePerHit = 10f;

    public override void Attack()
    {
        if (Owner == null || Target == null)
            return;

        CoroutineRunner.Run(LightningChainRoutine(Owner, Target));
    }

    private IEnumerator LightningChainRoutine(Transform startFrom, Transform firstTarget)
    {
        var currentSource = startFrom;
        var currentTarget = firstTarget;
        var hitTargets = new HashSet<Transform>();

        for (int i = 0; i < MaxBounces && currentTarget != null; i++)
        {
            ApplyDamage(currentTarget);

            if (LightningLine != null)
            {
                LightningLine.positionCount = 2;
                LightningLine.SetPosition(0, currentSource.position.AddY(1f));
                LightningLine.SetPosition(1, currentTarget.position.AddY(1f));
            }

            yield return new WaitForSeconds(LightningDuration);

            if (LightningLine != null)
            {
                LightningLine.positionCount = 0;
            }

            hitTargets.Add(currentTarget);

            var nextTarget = FindNextTarget(currentTarget, hitTargets);

            if (nextTarget == null)
                yield break;

            yield return new WaitForSeconds(ChainInterval);

            currentSource = currentTarget;
            currentTarget = nextTarget;
        }
    }

    private Transform FindNextTarget(Transform fromTarget, HashSet<Transform> alreadyHit)
    {
        var hits = Physics.OverlapSphere(fromTarget.position, ChainRadius, EnemyMask);
        Transform best = null;
        float bestSqrDist = float.MaxValue;

        foreach (var col in hits)
        {
            var t = col.transform;

            if (alreadyHit.Contains(t))
                continue;

            // var enemy = t.GetComponentInParent<Enemy>();
            // if (enemy == null) continue;

            var sqrDist = (t.position - fromTarget.position).sqrMagnitude;
            if (sqrDist < bestSqrDist)
            {
                bestSqrDist = sqrDist;
                best = t;
            }
        }

        return best;
    }

    private void ApplyDamage(Transform target)
    {
        // Тут должен быть код нанесения урона цели
        Debug.Log($"Lightning hit {target.name} for {DamagePerHit} damage.");
    }
}