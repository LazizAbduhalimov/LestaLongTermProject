using PoolSystem.Alternative;
using UnityEngine;

public class MissileSpawnAttackBase<T> : IAttack where T : MissileBase
{
    public float AttackRate;
    public Transform Owner;
    public Transform Target;
    public T MissilePrefab;
    public float MissileSpeed = 15f;

    protected float PassedTime;
    protected PoolService PoolService;
    protected PoolMono<T> MissilesPool;

    // КОНСТРУКТОР НЕ РАБОТАЕТ С ODIN INSPECTOR
    public void Init(PoolService poolService)
    {
        PoolService = poolService;
        MissilesPool = PoolService.GetOrRegisterPool(MissilePrefab, 10);
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

    public virtual void Attack()
    {
        var direction = (Target.position - Owner.position).WithY(0f);
        var rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        var missile = MissilesPool.GetFreeElement(Owner.position.AddY(1f), rotation, false);
        missile.Init(MissileSpeed);
        missile.gameObject.SetActive(true);
    }
}

public class PatternedMissileAttack : MissileSpawnAttackBase<PatternedTrailMissile>
{
    
}

public class AutoAimAttack : MissileSpawnAttackBase<AutoAimMissile>
{
    public override void Attack()
    {
        var direction = (Target.position - Owner.position).WithY(0f);
        var rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        var missile = MissilesPool.GetFreeElement(Owner.position.AddY(1f), rotation, false);
        missile.Init(MissileSpeed);
        missile.Target = Target;
        missile.gameObject.SetActive(true);
    }
}