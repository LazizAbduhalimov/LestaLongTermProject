using PoolSystem.Alternative;
using UnityEngine;

public interface IAttack
{
    public void Init(PoolService poolService, EnemiesNearbyFinder enemiesNearby, Transform owner);
    public void Update();
    public void Attack();
}
