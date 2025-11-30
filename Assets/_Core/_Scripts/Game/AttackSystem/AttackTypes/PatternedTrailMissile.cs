using Unity.VisualScripting;
using UnityEngine;

public class PatternedTrailMissile : MissileBase
{
    [Header("Trail Pattern")]
    public float Amplitude = 0.5f;
    public float Frequency = 4f;

    private float _phase;
    private Vector3 _initialForward;
    private Vector3 _initialRight;

    public override void OnEnable()
    {
        base.OnEnable();
        // Cache initial basis vectors to keep movement relative to the start orientation
        _initialForward = transform.forward;
        _initialRight = transform.right;
    }

    protected override void Update()
    {
        base.Update();

        var prevPos = transform.position;

        // Move forward relative to the initial orientation
        var forwardStep = _initialForward * Speed * Time.deltaTime;

        // Sinusoidal offset perpendicular to the initial forward
        _phase += Frequency * Time.deltaTime;
        var lateral = _initialRight * (Mathf.Sin(_phase) * Amplitude);

        // Apply movement
        var newPos = prevPos + forwardStep + lateral;
        transform.position = newPos;

        // Orient to face the actual movement direction (current velocity)
        var moveDir = (newPos - prevPos);
        if (moveDir.sqrMagnitude > 1e-6f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir.normalized, Vector3.up);
        }
    }
}
