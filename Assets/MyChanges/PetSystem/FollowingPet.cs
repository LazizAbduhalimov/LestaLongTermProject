using TMPro;
using UnityEngine;
using UnityEngine.Splines;


public interface IPet
{
    public Transform RestPosition { get; }

    public void InitialUpdate(Transform restPosition);
    public void UpdateBehavior();
}

public class FollowingPet : MonoBehaviour, IPet
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    public Transform RestPosition { get; private set; }

    public void InitialUpdate(Transform restPosition)
    {
        RestPosition = restPosition;
    }

    public void UpdateBehavior()
    {
        transform.position = Vector3.Lerp(transform.position, RestPosition.position, Time.deltaTime * _movementSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(RestPosition.forward);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );

    }
}
