using PoolSystem.Alternative;
using System.Collections;
using UnityEngine;


public class ShootingPet : MonoBehaviour, IPet
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDistance;
    [SerializeField] private float _calmdown;

    [SerializeField] private string _poolTag;

    private bool _canShoot;
    private Transform _target;

    private PoolContainer _bulletPool;
    public Transform RestPosition { get; private set; }


    public void InitialUpdate(Transform restPosition)
    {
        RestPosition = restPosition;

        GameObject.FindGameObjectWithTag(_poolTag).TryGetComponent<PoolContainer>(out PoolContainer container);
        _bulletPool = container;
        if (_bulletPool == null)
        {
            Debug.Log("chose right tag for pool");
        }
        _canShoot = true;

    }

    public void UpdateBehavior()
    {
        MoveMechanic();

        StartCoroutine(ShootMechanic());

    }

    
    private IEnumerator ShootMechanic()
    {
        if (!_canShoot)
            yield break;

        _canShoot = false;

        Collider[] objectsInZone = Physics.OverlapSphere(transform.position, _shootDistance);

        EnemyBrain targetEnemy = null;

        foreach (Collider collider in objectsInZone)
        {
            if (collider.TryGetComponent<EnemyBrain>(out EnemyBrain enemyBrain))
            {
                targetEnemy = enemyBrain;
                break;
            }
        }

        if (targetEnemy == null)
        {
            _canShoot = true;
            yield break;
        }

        _target = targetEnemy.transform;

        var obj = _bulletPool.Pool.GetFreeElement(true);
        obj.transform.position = transform.position;

        if (obj.TryGetComponent<TestDronBullet>(out TestDronBullet bullet))
        {
            bullet.BulletStart(_target.position, _bulletSpeed);
        }

        yield return new WaitForSeconds(_calmdown);

        _target = null;
        _canShoot = true;
    }

    private void MoveMechanic()
    {
        transform.position = Vector3.Lerp(transform.position, RestPosition.position, Time.deltaTime * _movementSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(_target == null ? RestPosition.transform.forward : _target.transform.position);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _shootDistance);
    }
}
