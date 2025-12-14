using PoolSystem.Alternative;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Спавнер который создает объекты на фиксированном расстоянии от таргета со случайным углом
/// </summary>
public class CircularSpawner : MonoBehaviour
{
    [Header("Target")]
    public Transform Target;
    
    [Header("Spawn Settings")]
    public float SpawnDistance = 10f;
    public float SpawnHeight = 0f;
    
    [Header("Prefabs")]
    public List<SpawnablePrefab> SpawnablePrefabs = new List<SpawnablePrefab>();
    
    [Header("Auto Spawn")]
    public bool AutoSpawn = false;
    public float SpawnInterval = 2f;
    
    private PoolService _poolService;
    private Dictionary<MonoBehaviour, PoolMono<MonoBehaviour>> _pools = new Dictionary<MonoBehaviour, PoolMono<MonoBehaviour>>();
    private float _nextSpawnTime;

    private void Start()
    {
        if (Target == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
                Target = player.transform;
        }

        // Создаем PoolService
        _poolService = new PoolService("CircularSpawner Pools");
        
        // Регистрируем все префабы в пулах
        foreach (var spawnable in SpawnablePrefabs)
        {
            if (spawnable.Prefab != null)
            {
                var pool = _poolService.RegisterPool(spawnable.Prefab, spawnable.PoolSize, null, spawnable.AutoExpand);
                _pools[spawnable.Prefab] = pool;
            }
        }
        
        _nextSpawnTime = Time.time + SpawnInterval;
    }

    private void Update()
    {
        if (AutoSpawn && Time.time >= _nextSpawnTime)
        {
            SpawnRandom();
            _nextSpawnTime = Time.time + SpawnInterval;
        }
    }

    /// <summary>
    /// Спавнит случайный префаб из списка
    /// </summary>
    public MonoBehaviour SpawnRandom()
    {
        if (SpawnablePrefabs.Count == 0)
        {
            Debug.LogWarning("No prefabs to spawn!");
            return null;
        }

        var randomIndex = Random.Range(0, SpawnablePrefabs.Count);
        return Spawn(SpawnablePrefabs[randomIndex].Prefab);
    }

    /// <summary>
    /// Спавнит конкретный префаб на случайной позиции
    /// </summary>
    public MonoBehaviour Spawn(MonoBehaviour prefab)
    {
        if (prefab == null || Target == null)
        {
            Debug.LogWarning("Prefab or Target is null!");
            return null;
        }

        if (!_pools.ContainsKey(prefab))
        {
            Debug.LogWarning($"Prefab {prefab.name} is not registered in pools!");
            return null;
        }

        Vector3 spawnPosition = GetRandomCircularPosition();
        return SpawnAt(prefab, spawnPosition);
    }

    /// <summary>
    /// Спавнит префаб на конкретной позиции
    /// </summary>
    public MonoBehaviour SpawnAt(MonoBehaviour prefab, Vector3 position)
    {
        if (!_pools.ContainsKey(prefab))
        {
            Debug.LogWarning($"Prefab {prefab.name} is not registered in pools!");
            return null;
        }

        var pool = _pools[prefab];
        var instance = pool.GetFreeElement(position, Quaternion.identity, false);
        if (instance.TryGetComponent<FollowTarget>(out var followTarget))
        {
            followTarget.Target = Target;
        }
        instance.gameObject.SetActive(true);
        
        return instance;
    }

    /// <summary>
    /// Спавнит префаб на случайном угле с заданным расстоянием
    /// </summary>
    public MonoBehaviour SpawnAtAngle(MonoBehaviour prefab, float angleInDegrees)
    {
        if (Target == null) return null;

        Vector3 position = GetCircularPosition(angleInDegrees);
        return SpawnAt(prefab, position);
    }

    /// <summary>
    /// Генерирует случайную позицию по кругу вокруг таргета
    /// </summary>
    private Vector3 GetRandomCircularPosition()
    {
        float randomAngle = Random.Range(0f, 360f);
        return GetCircularPosition(randomAngle);
    }

    /// <summary>
    /// Генерирует позицию по кругу на указанном угле
    /// </summary>
    private Vector3 GetCircularPosition(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        
        Vector3 offset = new Vector3(
            Mathf.Cos(angleInRadians) * SpawnDistance,
            SpawnHeight,
            Mathf.Sin(angleInRadians) * SpawnDistance
        );

        return Target.position + offset;
    }

    /// <summary>
    /// Спавнит несколько объектов равномерно по кругу
    /// </summary>
    public List<MonoBehaviour> SpawnMultiple(MonoBehaviour prefab, int count)
    {
        List<MonoBehaviour> spawnedObjects = new List<MonoBehaviour>();
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            var instance = SpawnAtAngle(prefab, angle);
            if (instance != null)
                spawnedObjects.Add(instance);
        }

        return spawnedObjects;
    }

    private void OnDrawGizmosSelected()
    {
        if (Target == null) return;

        Gizmos.color = Color.cyan;
        
        // Рисуем круг спавна
        int segments = 32;
        float angleStep = 360f / segments;
        
        Vector3 prevPoint = GetCircularPosition(0f);
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep;
            Vector3 nextPoint = GetCircularPosition(angle);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }

        // Рисуем крестик в центре таргета
        Gizmos.color = Color.red;
        Vector3 targetPos = Target.position + Vector3.up * SpawnHeight;
        Gizmos.DrawLine(targetPos + Vector3.left * 0.5f, targetPos + Vector3.right * 0.5f);
        Gizmos.DrawLine(targetPos + Vector3.forward * 0.5f, targetPos + Vector3.back * 0.5f);
    }
}

[System.Serializable]
public class SpawnablePrefab
{
    public MonoBehaviour Prefab;
    public int PoolSize = 10;
    public bool AutoExpand = true;
}
