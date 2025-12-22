using System.Collections;
using System.Collections.Generic;
using PoolSystem.Alternative;
using UnityEngine;

public class Attacker: MonoBehaviour
{
    public float EnemyDetectionRange = 30f;
    [Space(15f)]
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeReference] public List<AttackSO> AttackSOList;
    private List<IAttack> _attacks;

    private EnemiesNearbyFinder _enemiesNearby;
 
    public void Start()
    {
        var poolService = new PoolService("Pools");
        _enemiesNearby = new EnemiesNearbyFinder(_enemyLayerMask);
        
        // Создаем копии AttackType для каждого инстанса Attacker
        _attacks = AttackSOList.ConvertAll(attackSO => CloneAttackType(attackSO.AttackType));
        
        foreach (var attack in _attacks)
        {
            attack.Init(poolService, _enemiesNearby, transform);
        }        
        StartCoroutine(UpdateNearestEnemyCoroutine(0.5f));
    }
    
    private IAttack CloneAttackType(IAttack original)
    {
        if (original == null) return null;
        
        // Используем JsonUtility для глубокого клонирования
        string json = JsonUtility.ToJson(original);
        return (IAttack)JsonUtility.FromJson(json, original.GetType());
    }

    public IEnumerator UpdateNearestEnemyCoroutine(float updateInterval)
    {
        while (true)
        {
            _enemiesNearby.UpdateEnemiesInRange(transform.position, EnemyDetectionRange);
            _enemiesNearby.GetNearestEnemy(transform.position, true);
            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void Update()
    {
        foreach (var attack in _attacks)
        {
            attack.Update();
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetectionRange);
    }
}
