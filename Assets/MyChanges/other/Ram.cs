using UnityEngine;

public class Ram : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyBrain>(out EnemyBrain enemyBrain))
        {
            enemyBrain.Hit(gameObject);
        }
    }
}
