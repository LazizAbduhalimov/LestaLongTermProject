using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<string> BossKilled;
    public event Action<int> CoinsCollected;

    public void TriggerBossKilled(string bossName)
    {
        BossKilled?.Invoke(bossName);
    }

    public void TriggerCoinsCollected(int amount)
    {
        CoinsCollected?.Invoke(amount);
    }
}
