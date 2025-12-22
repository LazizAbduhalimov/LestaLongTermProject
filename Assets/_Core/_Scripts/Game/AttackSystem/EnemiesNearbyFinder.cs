using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesNearbyFinder
{
    public List<Transform> Enemies = new();   

    public LayerMask LayerMask;

    public EnemiesNearbyFinder(LayerMask layerMask)
    {
        LayerMask = layerMask;
    }

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
        var overlaps = Physics.OverlapSphere(position, range, LayerMask);
        Enemies = overlaps.Select(collider => collider.transform).ToList();
    }

    private Transform UpdateNearestEnemy(Vector3 position)
    {
        #if UNITY_EDITOR
        if (_nearestEnemy && _nearestEnemy.TryGetComponent<Enemy>(out var enemyComponent))
        {
            enemyComponent.DeactivateHighlight();
        }
        #endif

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

        #if UNITY_EDITOR
        if (nearest && nearest.TryGetComponent(out enemyComponent))
        {
            enemyComponent.ActivateHighlight();
        }
        #endif

        return _nearestEnemy = nearest;
    }
}