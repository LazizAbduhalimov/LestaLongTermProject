using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 15f;
    public float acceleration = 8f;
    public float rotationSpeed = 120f;
    
    [Header("Speed Settings")]
    public float boostMultiplier = 2f;
    
    [Header("Physics Settings")]
    public float dragCoefficient = 3f;
    public float centerOfMassY = -0.5f;

    [Header("View")]
    public Transform[] Wheels;
    public float maxWheelTurnAngle = 35f;
    public float wheelRotationSpeed = 5f;
    
    private Rigidbody carRigidbody;
    private float motorInput;
    private float steerInput;
    private bool isBoostPressed;
    private float currentWheelAngle;
    private Quaternion[] initialWheelRotations;
    
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = new Vector3(0, centerOfMassY, 0);
        
        // Сохраняем начальные ротации колес
        if (Wheels != null && Wheels.Length > 0)
        {
            initialWheelRotations = new Quaternion[Wheels.Length];
            for (int i = 0; i < Wheels.Length; i++)
            {
                if (Wheels[i] != null)
                {
                    initialWheelRotations[i] = Wheels[i].localRotation;
                }
            }
        }
    }
    
    void Update()
    {
        GetInput();
        HandleMovement();
        RotateWheels();
    }
    
    void FixedUpdate()
    {
        if (carRigidbody == null) return;
        
        ApplyDrag();
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
    
    void HandleMovement()
    {
        // Поворот через rigidbody angular velocity
        if (Mathf.Abs(steerInput) > 0.1f && Mathf.Abs(carRigidbody.linearVelocity.magnitude) > 0.5f)
        {
            float turnAmount = steerInput * rotationSpeed;
            carRigidbody.angularVelocity = new Vector3(0, turnAmount, 0);
        }
        else
        {
            // Останавливаем поворот когда нет ввода или стоим
            carRigidbody.angularVelocity = Vector3.Lerp(carRigidbody.angularVelocity, Vector3.zero, 5f * Time.deltaTime);


        }
        
        // Движение через velocity
        if (Mathf.Abs(motorInput) > 0.1f)
        {
            float currentMaxSpeed = maxSpeed;
            
            // Применяем буст если нажат Shift
            if (isBoostPressed)
            {
                currentMaxSpeed *= boostMultiplier;
            }
            
            // Вычисляем целевую скорость
            float targetSpeed = motorInput * currentMaxSpeed;
            
            // Плавно изменяем скорость В НАПРАВЛЕНИИ ТЕКУЩЕГО ПОВОРОТА МАШИНЫ
            Vector3 targetVelocity = transform.forward * targetSpeed;
            
            // Сохраняем вертикальную составляющую
            targetVelocity.y = carRigidbody.linearVelocity.y;
            
            // Плавно переходим к целевой скорости
            carRigidbody.linearVelocity = Vector3.Lerp(carRigidbody.linearVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
    }
    
    void ApplyDrag()
    {
        // Применяем сопротивление воздуха
        Vector3 horizontalVelocity = new Vector3(carRigidbody.linearVelocity.x, 0, carRigidbody.linearVelocity.z);
        
        if (Mathf.Abs(motorInput) < 0.1f)
        {
            // Сильное торможение когда нет ввода
            carRigidbody.linearVelocity = Vector3.Lerp(carRigidbody.linearVelocity, 
                new Vector3(0, carRigidbody.linearVelocity.y, 0), dragCoefficient * Time.fixedDeltaTime);
        }
        else
        {
            // Обычное сопротивление воздуха при движении
            Vector3 dragForce = -horizontalVelocity.normalized * horizontalVelocity.sqrMagnitude * 0.01f;
            carRigidbody.AddForce(dragForce);
        }
    }

    void RotateWheels()
    {
        if (Wheels == null || Wheels.Length == 0 || initialWheelRotations == null) return;

        // Целевой угол поворота колес на основе ввода
        float targetAngle = steerInput * maxWheelTurnAngle;

        // Плавно переходим к целевому углу
        currentWheelAngle = Mathf.Lerp(currentWheelAngle, targetAngle, wheelRotationSpeed * Time.deltaTime);

        // Применяем поворот ко всем колесам
        for (int i = 0; i < Wheels.Length; i++)
        {
            if (Wheels[i] != null)
            {
                // Применяем поворот относительно начальной ротации
                Quaternion steerRotation = Quaternion.Euler(0f, currentWheelAngle, 0f);
                Wheels[i].localRotation = steerRotation * initialWheelRotations[i];
            }
        }
    }
}