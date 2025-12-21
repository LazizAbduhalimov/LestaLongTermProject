using Game.Helpers;
using PoolSystem.Alternative;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] private int _eachPoolSize;
    [SerializeField] private bool _eachPoolAutoExpand;
    [SerializeField] private float _dropPositionDif;
    public static DropSystem Instance;
    private PoolService poolService;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        poolService = new PoolService("DropSystem");
    }
    public void Drop(DropTable table, Vector3 position)
    {
        foreach (var item in table.drops)
        {
            if (item.dropChance >= Random.Range(0f, 100f) )
            {
                int amount = Random.Range(item.minAmount, item.maxAmount + 1);

                for (int i = 0; i < amount; i++)
                {
                    

                    var pool = poolService.GetOrRegisterPool<AutoDeactivator>(item.prefab.GetComponent<AutoDeactivator>(), _eachPoolSize, gameObject.transform, _eachPoolAutoExpand);

                    pool.GetFreeElement(position + Random.insideUnitSphere * _dropPositionDif, Quaternion.identity, true);
                }
            }
        }
    }
}
