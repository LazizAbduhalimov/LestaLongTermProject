using UnityEngine;

public class CoinTask : BaseTask
{
    private int currentCoins;
    private int neededCoins;

    public CoinTask(string taskName, int currentCoins, int neededCoins) : base(taskName)
    {
        this.currentCoins = currentCoins;
        this.neededCoins = neededCoins;

    }

    public override void StartTask()
    {
        GameEvents.Instance.CoinsCollected += AddCoins;
    }

    public void AddCoins(int coins)
    {
        currentCoins += coins;
        UpdateTask();
    }

    public override void UpdateTask()
    {
        if(currentCoins >= neededCoins) CompleteTask();
    }

    public override void CompleteTask()
    {
        GameEvents.Instance.CoinsCollected -= AddCoins;
        base.CompleteTask();

    }
}
