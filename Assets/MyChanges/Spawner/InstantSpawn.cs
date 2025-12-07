using PoolSystem.Alternative;
using UnityEngine;

public class InstantSpawn : MonoBehaviour, ISpawnStrategy
{
    private PoolContainer _pool;
    private int _count;

    public InstantSpawn(PoolContainer enemyContainer, int count) 
    {
        _pool = enemyContainer;
        _count = count;
    }

    public void Spawn(Transform[] spawnPoints)
    {
        for (int i = 0; i < _count; i++)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemy = _pool.Pool.GetFreeElement(true);

            enemy.transform.position = point.position;

        }
    }
}
