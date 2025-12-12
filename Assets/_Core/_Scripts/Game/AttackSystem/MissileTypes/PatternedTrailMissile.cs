using UnityEngine;

public class PatternedTrailMissile : MissileBase
{
    [Header("Trail Pattern")]
    private Rigidbody _rb;
    public float Amplitude = 0.5f;
    public float Frequency = 4f;

    private float _phase;
    private Vector3 _initialForward;
    private Vector3 _initialRight;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _phase = 0f;
    }

    protected void FixedUpdate()
    {
        if (_rb == null) return;

        // Обновляем направление каждый кадр для корректной работы после респауна
        if (_phase == 0f)
        {
            _initialForward = transform.forward;
            _initialRight = transform.right;
        }

        var prevPos = _rb.position;
        var forwardStep = _initialForward * Speed * Time.fixedDeltaTime;
        _phase += Frequency * Time.fixedDeltaTime;
        var lateral = _initialRight * (Mathf.Sin(_phase) * Amplitude);
        var newPos = prevPos + forwardStep + lateral;
        _rb.MovePosition(newPos);
        var moveDir = newPos - prevPos;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir.normalized, Vector3.up);
        }
    }
}
