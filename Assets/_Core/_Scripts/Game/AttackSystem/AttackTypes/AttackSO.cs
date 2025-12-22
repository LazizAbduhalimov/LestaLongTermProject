using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackType", menuName = "AttackType/AttackSO", order = -10000)]
public class AttackSO : ScriptableObject
{
    [SerializeReference] public IAttack AttackType;
}