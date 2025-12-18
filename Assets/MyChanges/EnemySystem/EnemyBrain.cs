using PoolSystem.Alternative;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private IEnemyBehaviour[] behaviours;

    private void Awake()
    {
        behaviours = GetComponents<IEnemyBehaviour>();
    }

    private void Update()
    {
        foreach (var b in behaviours)
            b.Tick();
    }
    private void FixedUpdate()
    {
        foreach (var b in behaviours)
            b.TickFixedUpdate();
    }

    public void Hit(GameObject agressor)
    {
        foreach (var b in behaviours)
            b.OnHit(agressor);
    }
}
