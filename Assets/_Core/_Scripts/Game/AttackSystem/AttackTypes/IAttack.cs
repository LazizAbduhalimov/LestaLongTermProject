using PoolSystem.Alternative;

public interface IAttack
{
    public void Init(PoolService poolService, EnemiesNearbyFinder enemiesNearby);
    public void Update();
    public void Attack();
}
