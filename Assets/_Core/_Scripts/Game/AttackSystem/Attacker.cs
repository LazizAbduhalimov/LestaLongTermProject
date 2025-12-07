using System.Collections.Generic;
using PoolSystem.Alternative;
using UnityEngine;

public class Attacker: MonoBehaviour
{
    [SerializeReference] public List<IAttack> AttackType;
 
    public void Start()
    {
        var poolService = new PoolService("Pools");
        foreach (var attack in AttackType)
        {
            attack.Init(poolService);
        }        
    }

    public void Update()
    {
        foreach (var attack in AttackType)
        {
            attack.Update();
        }
    }
}