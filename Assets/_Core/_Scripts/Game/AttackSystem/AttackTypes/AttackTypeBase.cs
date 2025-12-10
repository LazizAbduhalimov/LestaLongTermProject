using PoolSystem.Alternative;
using UnityEngine;

public abstract class AttackTypeBase : IAttack
{
    public Transform Owner;
    public Transform Target;
    public float AttackRate;
    protected float PassedTime;
    protected PoolService PoolService;

    public virtual void Init(PoolService poolService)
    {
        PoolService = poolService;
    }

    public virtual void Update()
    {
        PassedTime += Time.deltaTime;
        if (PassedTime >= AttackRate)
        {
            Attack();
            PassedTime = 0;
        }
    }

    public virtual void Attack() {}
}
