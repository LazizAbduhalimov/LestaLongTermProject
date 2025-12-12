using PoolSystem.Alternative;
using UnityEngine;

public class MissileSpawnAttackBase<T> : AttackTypeBase where T : MissileBase
{
    public T MissilePrefab;
    public float MissileSpeed = 15f;
    protected PoolMono<T> MissilesPool;

    public override void Init(PoolService poolService, EnemiesNearby enemiesNearby)
    {
        base.Init(poolService, enemiesNearby);
        MissilesPool = PoolService.GetOrRegisterPool(MissilePrefab, 10);
    }

    public override void Attack()
    {
        if (Target == null || Owner == null)
        {
            Debug.LogWarning("Target or Owner is null in Attack()");
            return;
        }

        var direction = (Target.position - Owner.position).WithY(0f);
        var rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        Debug.DrawRay(Owner.position, direction.normalized * 5f, Color.yellow, 2f);
        
        var missile = MissilesPool.GetFreeElement(Owner.position.AddY(1f), rotation, false);
        missile.Init(MissileSpeed);
        missile.gameObject.SetActive(true);
    }
}
