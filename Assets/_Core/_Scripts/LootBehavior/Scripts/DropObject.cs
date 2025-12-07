using System;
using UnityEngine;

public interface IDrop
{
    public void OnDropped();
    public void OnPickedUp();
}

public class DropObject : MonoBehaviour, IDrop
{
    [SerializeField] private bool _canBeAttracted;

    public void OnDropped()
    {
        //кака€-то логика при спавне дропа, например дл€ анимации 
        Console.WriteLine("„то-то упало");
    }

    public void OnPickedUp()
    {
        //кака€-то логика при подборе
        gameObject.SetActive(false);
    }
}
