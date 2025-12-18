using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarControllerForce : MonoBehaviour
{
    [Header("Movement Settings")]
    public float motorForce = 1000f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;
    
    [Header("Speed Settings")]
    public float boostMultiplier = 2f;
    
    [Header("Physics Settings")]
    public float downforce = 100f;
    public float centerOfMassY = -0.5f;
    
    private Rigidbody carRigidbody;
    private float motorInput;
    private float steerInput;
    private bool isBoostPressed;
    
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        if (carRigidbody == null)
        {
            Debug.LogError("CarControllerForce requires a Rigidbody component!");
            return;
        }
        
        // Устанавливаем центр масс ниже для лучшей устойчивости
        carRigidbody.centerOfMass = new Vector3(0, centerOfMassY, 0);
    }
    
    void Update()
    {
        GetInput();
    }
    
    void FixedUpdate()
    {
        if (carRigidbody == null) return;
        
        HandleMotor();
        HandleSteering();
        ApplyDownforce();
    }
    
    void GetInput()
    {
        // W/S для движения вперед/назад
        motorInput = 0f;
        if (Input.GetKey(KeyCode.W))
            motorInput = 1f;
        else if (Input.GetKey(KeyCode.S))
            motorInput = -1f;
        
        // A/D для поворотов
        steerInput = 0f;
        if (Input.GetKey(KeyCode.A))
            steerInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            steerInput = 1f;
        
        // Shift для ускорения
        isBoostPressed = Input.GetKey(KeyCode.LeftShift);
    }
    
    void HandleMotor()
    {
        float currentMotorForce = motorForce;
        
        // Применяем буст если нажат Shift
        if (isBoostPressed)
        {
            currentMotorForce *= boostMultiplier;
        }
        
        // Применяем силу движения
        Vector3 forceVector = transform.forward * motorInput * currentMotorForce;
        carRigidbody.AddForce(forceVector);
        
        // Применяем торможение когда нет ввода
        if (Mathf.Abs(motorInput) < 0.1f)
        {
            carRigidbody.linearDamping = 3f;
        }
        else
        {
            carRigidbody.linearDamping = 0.3f;
        }
    }
    
    void HandleSteering()
    {
        // Поворот только через rigidbody, без transform.Rotate
        if (Mathf.Abs(steerInput) > 0.1f && Mathf.Abs(carRigidbody.linearVelocity.magnitude) > 0.1f)
        {
            float steerTorque = steerInput * maxSteerAngle * carRigidbody.linearVelocity.magnitude * 10f;
            carRigidbody.AddTorque(transform.up * steerTorque);
        }
        
        // Угловое торможение для более реалистичного поведения
        carRigidbody.angularDamping = 5f;
    }
    
    void ApplyDownforce()
    {
        // Прижимная сила для лучшего сцепления на высокой скорости
        float speedFactor = carRigidbody.linearVelocity.magnitude / 10f;
        Vector3 downforceVector = -transform.up * downforce * speedFactor;
        carRigidbody.AddForce(downforceVector);
    }
}