using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpiralHomingMissile : MissileBase
{
    [Header("Targeting")]
    public Transform Target;
    public float TurnRatePerSecond = 90f;
    
    [Header("Spiral Motion")]
    public float SpiralRadius = 2f;
    public float SpiralSpeed = 3f;
    public float UpwardForce = 5f;
    public float UpwardDuration = 1f;
    
    [Header("Speed Acceleration")]
    public float MaxSpeed = 30f;
    public float AccelerationDuration = 2f;
    
    private Rigidbody _rb;
    private float _spiralAngle;
    private float _timeAlive;
    private bool _isUpwardPhase = true;
    private float _currentSpeed;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _spiralAngle = 0f;
        _timeAlive = 0f;
        _isUpwardPhase = true;
        _currentSpeed = Speed;
    }

    public override void Init(float speed, float lifeTime = 5f)
    {
        base.Init(speed, lifeTime);
        _spiralAngle = 0f;
        _timeAlive = 0f;
        _isUpwardPhase = true;
        _currentSpeed = Speed;
    }

    protected void FixedUpdate()
    {
        if (_rb == null) return;

        _timeAlive += Time.fixedDeltaTime;

        // Увеличиваем скорость со временем (cubic sine easing)
        UpdateSpeed();

        // Проверяем, закончилась ли фаза полета вверх
        if (_timeAlive > UpwardDuration)
        {
            _isUpwardPhase = false;
        }

        if (_isUpwardPhase)
        {
            // Фаза полета вверх со спиралью
            Vector3 upwardDirection = CalculateSpiralUpwardDirection();
            
            // Плавно поворачиваем ракету в направлении движения
            if (upwardDirection.sqrMagnitude > 0.001f)
            {
                var targetRotation = Quaternion.LookRotation(upwardDirection, Vector3.up);
                _rb.MoveRotation(Quaternion.RotateTowards(_rb.rotation, targetRotation, TurnRatePerSecond * 2f * Time.fixedDeltaTime));
            }
            
            // Двигаемся в направлении, куда смотрит ракета
            var nextPos = _rb.position + _rb.rotation * Vector3.forward * _currentSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(nextPos);
        }
        else
        {
            // Фаза наведения на цель
            RotateTowardsTarget();
            
            // Двигаемся вперед по направлению ракеты
            var nextPos = _rb.position + _rb.rotation * Vector3.forward * _currentSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(nextPos);
        }
    }

    private void UpdateSpeed()
    {
        if (_timeAlive >= AccelerationDuration)
        {
            _currentSpeed = MaxSpeed;
            return;
        }

        // Cubic sine easing: sin(t * π/2)^3
        float t = _timeAlive / AccelerationDuration;
        float easedT = Mathf.Pow(Mathf.Sin(t * Mathf.PI * 0.5f), 3f);
        _currentSpeed = Mathf.Lerp(Speed, MaxSpeed, easedT);
    }

    private Vector3 CalculateSpiralUpwardDirection()
    {
        // Увеличиваем угол спирали
        _spiralAngle += SpiralSpeed * Time.fixedDeltaTime;

        // Базовое направление - вверх и немного вперед
        Vector3 baseDirection = (Vector3.up * UpwardForce + Vector3.forward * 2f).normalized;
        
        // Добавляем спиральное отклонение
        float spiralOffset = Mathf.Sin(_spiralAngle) * SpiralRadius * 0.3f;
        Vector3 rightOffset = Vector3.right * spiralOffset;
        
        return (baseDirection + rightOffset).normalized;
    }

    private void RotateTowardsTarget()
    {
        if (Target == null) return;

        // Направление к цели
        Vector3 toTarget = (Target.position - _rb.position);
        
        if (toTarget.sqrMagnitude < 0.001f) return;
        
        // Желаемая ротация к цели
        Quaternion targetRotation = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
        
        // Плавно поворачиваем к цели
        Quaternion newRotation = Quaternion.RotateTowards(
            _rb.rotation, 
            targetRotation, 
            TurnRatePerSecond * Time.fixedDeltaTime
        );
        
        _rb.MoveRotation(newRotation);
    }
}
