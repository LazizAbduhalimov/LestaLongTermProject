using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TopDownCarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 25f;   
    [SerializeField] private float maxSpeed = 30f;           
    [SerializeField] private float deceleration = 20f;      
    
    [SerializeField] private float turnSpeed = 220f;         
    [SerializeField] private float driftFactor = 0.85f;      

    private Rigidbody rb;
    private float inputVertical;
    private float inputHorizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 1.5f;
        rb.angularDamping = 3f;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        inputVertical = Input.GetAxis("Vertical");    
        inputHorizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.forward;
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, forward);

        if (inputVertical != 0f)
        {
            rb.AddForce(forward * inputVertical * acceleration, ForceMode.Acceleration);
        }
        else
        {
            Vector3 brake = -forward * Mathf.Min(forwardSpeed, deceleration);
            rb.AddForce(brake, ForceMode.Acceleration);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void HandleRotation()
    {
        if (rb.linearVelocity.magnitude < 0.1f) return;

        float speedPercent = Mathf.Clamp(rb.linearVelocity.magnitude / maxSpeed, 0f, 1f);
        float turn = inputHorizontal * turnSpeed * speedPercent * Time.fixedDeltaTime;

        Quaternion rotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * rotation);

        Vector3 forwardVelocity = transform.forward * Vector3.Dot(rb.linearVelocity, transform.forward);
        Vector3 lateralVelocity = rb.linearVelocity - forwardVelocity;

        lateralVelocity *= driftFactor;

        rb.linearVelocity = forwardVelocity + lateralVelocity;
    }
}