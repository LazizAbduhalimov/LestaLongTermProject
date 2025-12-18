using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class TestDronBullet : MonoBehaviour 
{
    [SerializeField] private float _lifetime;

    private float _speed;
    private Vector3 _direction;

   private Rigidbody _rb;


    public void BulletStart(Vector3 targetPoint, float speed)
    {

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();

        }

        _direction = targetPoint;
        _speed = speed;

        Vector3 dir = (_direction - transform.position).normalized;

        transform.forward = (targetPoint - transform.position).normalized;
        _rb.linearVelocity = dir * _speed;

        gameObject.SetActive(true);

        StartCoroutine(AutoDelete());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyBrain>(out EnemyBrain enemyBrain))
        {
            enemyBrain.Hit(gameObject);

            _rb.linearVelocity = Vector3.zero;
            gameObject.SetActive(false);

        }
    }

    IEnumerator AutoDelete()
    {
        yield return new WaitForSeconds( _lifetime );
        _rb.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
