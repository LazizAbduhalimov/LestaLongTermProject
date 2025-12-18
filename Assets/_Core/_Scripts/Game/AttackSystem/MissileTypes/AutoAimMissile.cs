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
            var toTarget = (Target.position - _rb.position).WithY(0f);
            if (toTarget.sqrMagnitude > 0.001f)
            {
                var desired = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
                var newRotation = Quaternion.RotateTowards(_rb.rotation, desired, TurnRatePerSecond * Time.fixedDeltaTime);
                _rb.MoveRotation(newRotation);
            }
        }

        var nextPos = _rb.position + _rb.rotation * Vector3.forward * Speed * Time.fixedDeltaTime;
        _rb.MovePosition(nextPos);
    }
}
