using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PoolSystem.Alternative;
using UnityEngine;

public class Attacker: MonoBehaviour
{
    public float EnemyDetectionRange = 30f;
    [Space(15f)]
    [SerializeReference] public List<IAttack> AttackType;
    
    private EnemiesNearby _enemiesNearby;
 
    public void Start()
    {
        var poolService = new PoolService("Pools");
        _enemiesNearby = new EnemiesNearby();
        
        foreach (var attack in AttackType)
        {
            attack.Init(poolService, _enemiesNearby);
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

public class EnemiesNearby
{
    public List<Transform> Enemies = new();   

    private LayerMask _layerMask = LayerMask.GetMask("Enemy");
    private Transform _nearestEnemy;


    public Transform GetNearestEnemy(Vector3 position, bool forceUpdate = false)
    {
        if (_nearestEnemy == null || !_nearestEnemy.gameObject.activeInHierarchy || forceUpdate)
        {
            return UpdateNearestEnemy(position);
        }

        return _nearestEnemy;
    }

    public void UpdateEnemiesInRange(Vector3 position, float range)
    {
        var overlaps = Physics.OverlapSphere(position, range, _layerMask);
        Enemies = overlaps.Select(collider => collider.transform).ToList();
    }

    private Transform UpdateNearestEnemy(Vector3 position)
    {
        if (Enemies.Count == 0)
        {
            _nearestEnemy = null;
            return null;
        }

        Transform nearest = null;
        float minSqrDistance = float.MaxValue;

        foreach (var enemy in Enemies)
        {
            if (enemy == null) continue;

            var sqrDist = (enemy.position - position).sqrMagnitude;
            if (sqrDist < minSqrDistance)
            {
                minSqrDistance = sqrDist;
                nearest = enemy;
            }
        }

        return _nearestEnemy = nearest;
    }
}