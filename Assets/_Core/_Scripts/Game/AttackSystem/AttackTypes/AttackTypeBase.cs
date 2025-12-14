using PoolSystem.Alternative;
using UnityEngine;

public abstract class AttackTypeBase : IAttack
{
    public Transform Owner;
    public float AttackRate;
    
    protected Transform Target;
    protected float PassedTime;
    protected PoolService PoolService;
    protected EnemiesNearbyFinder EnemiesNearby;

    public virtual void Init(PoolService poolService, EnemiesNearbyFinder enemiesNearby, Transform owner)
    {
        PoolService = poolService;
        EnemiesNearby = enemiesNearby;
        Owner = owner;
    }

    public virtual void Update()
    {
        PassedTime += Time.deltaTime;
        if (PassedTime >= AttackRate)
        {
            Target = EnemiesNearby.GetNearestEnemy(Owner.position);
            if (Target == null) return;
            Attack();
            PassedTime = 0;
        }
    }

    public virtual void Attack() {}
}
