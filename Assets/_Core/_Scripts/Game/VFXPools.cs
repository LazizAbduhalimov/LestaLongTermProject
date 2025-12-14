using PoolSystem.Alternative;
using UnityEngine;

// пока временно
public class VFXPools : MonoBehaviour
{
    public PoolContainer FireImpactPool;
    public PoolContainer ExplosionImpactPool;

    public static VFXPools Instance => _instance;
    private static VFXPools _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}