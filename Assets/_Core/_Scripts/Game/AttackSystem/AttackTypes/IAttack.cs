using PoolSystem.Alternative;

public interface IAttack
{
    public void Init(PoolService poolService);
    public void Update();
    public void Attack();
}
