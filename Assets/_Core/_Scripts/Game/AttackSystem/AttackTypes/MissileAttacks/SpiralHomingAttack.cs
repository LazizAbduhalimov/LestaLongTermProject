using UnityEngine;

public class SpiralHomingAttack : MissileSpawnAttackBase<SpiralHomingMissile>, IAttack
{
    [Header("Spiral Homing Settings")]
    public float TurnRatePerSecond = 90f;
    public float SpiralRadius = 2f;
    public float SpiralSpeed = 3f;
    public float UpwardForce = 5f;
    public float UpwardDuration = 1f;

    public override void Attack()
    {
        if (Target == null || Owner == null)
        {
            Debug.LogWarning("Target or Owner is null in SpiralHomingAttack");
            return;
        }

        // Начальная позиция чуть выше владельца
        Vector3 spawnPosition = Owner.position.AddY(1f);
        
        // Начальная ротация - смотрит вверх с небольшим наклоном вперед
        Quaternion spawnRotation = Quaternion.Euler(45f, Owner.eulerAngles.y, 0f);
        
        var missile = MissilesPool.GetFreeElement(spawnPosition, spawnRotation, false);
        
        // Настраиваем параметры ракеты
        missile.Init(MissileSpeed);
        missile.Target = Target;
        missile.TurnRatePerSecond = TurnRatePerSecond;
        missile.SpiralRadius = SpiralRadius;
        missile.SpiralSpeed = SpiralSpeed;
        missile.UpwardForce = UpwardForce;
        missile.UpwardDuration = UpwardDuration;
        
        AssignLayer(Owner.gameObject.layer, missile.gameObject);
        missile.gameObject.SetActive(true);
    }
}
