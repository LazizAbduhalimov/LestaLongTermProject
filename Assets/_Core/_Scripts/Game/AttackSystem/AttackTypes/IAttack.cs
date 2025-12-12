using PoolSystem.Alternative;

public interface IAttack
{
    public void Init(PoolService poolService, EnemiesNearby enemiesNearby);
    public void Update();
    public void Attack();
}
