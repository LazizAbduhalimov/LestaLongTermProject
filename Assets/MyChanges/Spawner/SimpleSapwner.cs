using PoolSystem.Alternative;
using System.Collections;
using UnityEngine;

public class SimpleSapwner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;

    [SerializeField] private PoolContainer _enemyData;

    private bool _canSpawn = true;

    public void FixedUpdate()
    {
        if (_canSpawn)
        {
            StartCoroutine(spawn());
        }
    }
    IEnumerator spawn()
    {
        _canSpawn = false;

        var enemy = _enemyData.Pool.GetFreeElement();
        Instantiate(enemy, new Vector3(0,0,0), Quaternion.identity);

        yield return new WaitForSeconds(_spawnDelay);

        _canSpawn = true;
    }
}
