using UnityEngine;

public class MissileColisionProvider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            Debug.Log("Hit enemy" + enemy.name);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Hit non-enemy object: " + other.name);
        }
    }
}