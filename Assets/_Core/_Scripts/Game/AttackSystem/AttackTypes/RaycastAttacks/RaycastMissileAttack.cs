using System.Collections;
using Modules.Coroutines;
using UnityEngine;

public class RaycastMissileAttack : AttackTypeBase
{
    public LineRenderer RayLine;
    public float RayDistance = 50f;
    public float RayRadius = 0.5f;
    public LayerMask RayMask = ~0;

    public override void Attack()
    {
        if (Owner == null || Target == null)
            return;

        var direction = (Target.position - Owner.position).WithY(0f).normalized;
        var origin = Owner.position.AddY(1f);

        var hits = Physics.SphereCastAll(origin, RayRadius, direction, RayDistance, RayMask, QueryTriggerInteraction.Ignore);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var hit in hits)
        {
            Debug.Log("Ray hit: " + hit.transform.name);
        }

        if (RayLine != null)
        {
            RayLine.widthCurve = new AnimationCurve(new Keyframe(0, RayRadius), new Keyframe(1, RayRadius));
            RayLine.positionCount = 2;
            RayLine.SetPosition(0, origin);
            RayLine.SetPosition(1, origin + direction * RayDistance);
            CoroutineRunner.Run(AttackRayEffectDisableAfterDelay(0.3f));
        }

        Debug.DrawRay(origin, direction * RayDistance, Color.red, 0.5f);
    }

    private IEnumerator AttackRayEffectDisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (RayLine != null)
        {
            RayLine.positionCount = 0;
        }
    }
}
