using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PatternedTrailMissile : MissileBase
{
    [Header("Trail Pattern")]
    private Rigidbody _rb;
    public float Amplitude = 1f;
    public float NoiseScale = 2f; // Частота шума
    public float NoiseSpeed = 3f; // Скорость изменения

    private float _time;
    private float _noiseOffsetX;
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
        _time = 0f;
        _initialForward = transform.forward;
        _initialRight = transform.right;
        
        _noiseOffsetX = Random.Range(0f, 100f);
    }

    protected void FixedUpdate()
    {
        if (_rb == null) return;

        _time += Time.fixedDeltaTime * NoiseSpeed;

        var prevPos = _rb.position;
        var forwardStep = _initialForward * Speed * Time.fixedDeltaTime;
        
        var noiseX = (Mathf.PerlinNoise(_time * NoiseScale + _noiseOffsetX, 0f) - 0.5f) * 2f;
        
        var lateral = _initialRight * noiseX * Amplitude;
        
        var newPos = prevPos + forwardStep + lateral;
        _rb.MovePosition(newPos);
        
        var moveDir = newPos - prevPos;
        if (moveDir.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir.normalized, Vector3.up);
        }
    }
}
