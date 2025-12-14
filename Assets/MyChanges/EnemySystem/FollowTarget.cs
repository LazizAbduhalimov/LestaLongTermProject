using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowTarget : MonoBehaviour
{
    public Transform Target;
    public float MoveSpeed = 5f;
    public float TurnSpeed = 90f;
    
    [Header("Wheels")]
    public Transform[] Wheels;
    public float MaxWheelAngle = 45f;
    public float WheelTurnSpeed = 180f; // градусов в секунду (90° за 0.5с = 180°/с)

    private Rigidbody _rb;
    private float _currentWheelAngle = 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rb == null) return;

        float targetWheelAngle = 0f;

        // Поворот к цели
        if (Target != null)
        {
            var toTarget = (Target.position - _rb.position).WithY(0f);
            if (toTarget.sqrMagnitude > 0.001f)
            {
                var desired = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
                var currentRotation = _rb.rotation;
                var newRotation = Quaternion.RotateTowards(currentRotation, desired, TurnSpeed * Time.fixedDeltaTime);
                _rb.MoveRotation(newRotation);

                // Вычисляем желаемый угол поворота колёс
                float angleDiff = Quaternion.Angle(currentRotation, desired);
                Vector3 cross = Vector3.Cross(currentRotation * Vector3.forward, toTarget.normalized);
                float direction = Mathf.Sign(cross.y);
                
                targetWheelAngle = Mathf.Clamp(angleDiff * direction, -MaxWheelAngle, MaxWheelAngle);
            }
        }

        // Плавно поворачиваем колёса к целевому углу
        _currentWheelAngle = Mathf.MoveTowards(_currentWheelAngle, targetWheelAngle, WheelTurnSpeed * Time.fixedDeltaTime);

        if (Wheels != null)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel != null)
                {
                    wheel.localRotation = Quaternion.Euler(0f, _currentWheelAngle, 0f);
                }
            }
        }

        // Движение вперёд
        var nextPos = _rb.position + _rb.rotation * Vector3.forward * MoveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(nextPos);
    }
}
