using UnityEngine;

public interface ISpawnStrategy
{
    void Spawn(Transform[] spawnPoints);
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    public static EnemySpawner Instance;

    private ISpawnStrategy _strategy;

    private void Awake()
    {
        Instance = this;
    }

    public void SetStrategy(ISpawnStrategy strategy)
    {
        _strategy = strategy;
    }


    public void SpawnEnemies()
    {
        _strategy?.Spawn(spawnPoints);
    }
}
