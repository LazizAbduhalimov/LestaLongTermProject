using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AutoAimMissile : MissileBase
{
    public Transform Target;
    public float TurnRatePerSecond = 180f;
    private Rigidbody _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        if (_rb == null) return;

        if (Target != null)
        {
            var toTarget = (Target.position - transform.position).WithY(0f).normalized;
            var desired = Quaternion.LookRotation(toTarget, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, TurnRatePerSecond * Time.fixedDeltaTime);
        }

        var nextPos = _rb.position + transform.forward * Speed * Time.fixedDeltaTime;
        _rb.MovePosition(nextPos);
    }
}
