using PoolSystem.Alternative;
using UnityEngine;

public class MissileSpawnAttackBase<T> : AttackTypeBase where T : MissileBase
{
    public T MissilePrefab;
    public float MissileSpeed = 15f;
    protected PoolMono<T> MissilesPool;

    public override void Init(PoolService poolService)
    {
        base.Init(poolService);
        MissilesPool = PoolService.GetOrRegisterPool(MissilePrefab, 10);
    }

    public override void Attack()
    {
        var direction = (Target.position - Owner.position).WithY(0f);
        var rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        var missile = MissilesPool.GetFreeElement(Owner.position.AddY(1f), rotation, false);
        missile.Init(MissileSpeed);
        missile.gameObject.SetActive(true);
    }
}
