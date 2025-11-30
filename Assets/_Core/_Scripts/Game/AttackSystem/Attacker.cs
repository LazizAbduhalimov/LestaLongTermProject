using PoolSystem.Alternative;
using UnityEngine;

public class Attacker: MonoBehaviour
{
    public float AttackRate = 1;
    private float _passedTime;
    public MissileBase MissilePrefab;
    private PoolService _poolService;
    private PoolMono<MissileBase> _missilePools;
    private Enemy _enemy;
 
    public void Start()
    {
        _poolService = new PoolService("Pools");
        _missilePools = _poolService.GetOrRegisterPool(MissilePrefab, 10);    
        _enemy = FindAnyObjectByType<Enemy>();
    }

    public void Update()
    {
        _passedTime += Time.deltaTime;
        if (_passedTime >= AttackRate)
        {
            Attack();
            _passedTime = 0;
        }
    }
    
    public void Attack()
    {
        var missile = _missilePools.GetFreeElement(false);
        missile.Init(Random.Range(10, 20));
        missile.transform.position = transform.position.AddY(1f);
        // missile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), Random.Range(0, 360));
        // Горизонтальное направление (Y=0) к врагу
        var direction = _enemy.transform.position - missile.transform.position;
        direction.y = 0f; // игнорируем высоту
        missile.transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        // missile.transform.rotation 
        missile.gameObject.SetActive(true);
    }
}