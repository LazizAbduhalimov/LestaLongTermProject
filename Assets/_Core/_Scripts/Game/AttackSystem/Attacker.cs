using System;
using System.Collections;
using System.Collections.Generic;
using PoolSystem.Alternative;
using UnityEngine;

public class Attacker: MonoBehaviour
{
    public float EnemyDetectionRange = 30f;
    [Space(15f)]
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeReference] public List<IAttack> AttackType;
    private EnemiesNearbyFinder _enemiesNearby;
 
    public void Start()
    {
        var poolService = new PoolService("Pools");
        _enemiesNearby = new EnemiesNearbyFinder();
        _enemiesNearby.LayerMask = _enemyLayerMask;
        
        foreach (var attack in AttackType)
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
        foreach (var attack in AttackType)
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
