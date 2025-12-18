using NUnit.Framework;
using PoolSystem.Alternative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f; // ������ 5 ������
    [SerializeField] private List<PoolContainer> _pools;
    

    private void Start()
    {
        EnemySpawner.Instance.SetStrategy(new InstantSpawn(_pools[0], 5));
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            EnemySpawner.Instance.SpawnEnemies(); 
            yield return new WaitForSeconds(_spawnInterval); 
        }
    }
}

