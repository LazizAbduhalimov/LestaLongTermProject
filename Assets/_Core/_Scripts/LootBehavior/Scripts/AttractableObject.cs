using UnityEngine;

interface IAttractable
{
    public void AttractTo(Vector3 target, float speed);
}

public class AttractableObject : MonoBehaviour, IAttractable
{
    public void AttractTo(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
