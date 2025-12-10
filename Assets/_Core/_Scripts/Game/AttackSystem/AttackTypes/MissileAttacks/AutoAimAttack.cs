using UnityEngine;

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
