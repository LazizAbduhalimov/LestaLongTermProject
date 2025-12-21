using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject prefab;
    [Range(0, 100)] public float dropChance;
    public int minAmount;
    public int maxAmount;

}
