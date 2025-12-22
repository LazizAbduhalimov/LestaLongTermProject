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
        _attacks = AttackSOList.ConvertAll(attackSO => attackSO.AttackType);       
        foreach (var attack in _attacks)
        {
            attack.Init(poolService, _enemiesNearby, transform);
        }        
        StartCoroutine(UpdateNearestEnemyCoroutine(0.5f));
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
