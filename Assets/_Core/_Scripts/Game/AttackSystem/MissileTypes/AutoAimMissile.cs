using UnityEngine;

public class AutoAimMissile : MissileBase
{
    [Header("Auto Aim")]
    public Transform Target;
    public float TurnRate = 180f; // degrees per second

    protected override void Update()
    {
        base.Update();

        if (Target != null)
        {
            var toTarget = (Target.position - transform.position).WithY(0f).normalized;
            var desired = Quaternion.LookRotation(toTarget, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, TurnRate * Time.deltaTime);
        }

        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}
