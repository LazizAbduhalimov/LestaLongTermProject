using PoolSystem.Alternative;
using UnityEngine;

public abstract class MissileBase : PoolObject
{
    [Header("Movement")]
    public float Speed = 10f;

    [Tooltip("Optional lifetime in seconds. 0 disables auto-destroy.")]
    public float LifeTime = 0f;

    private float _alive;

    public virtual void Init(float speed, float lifeTime = 5f)
    {
        Speed = speed;
        LifeTime = lifeTime;
    }

    public virtual void OnEnable()
    {
        _alive = 0f;
    }

    public virtual void OnDisable()
    {
        transform.rotation = Quaternion.identity;
    }

    protected virtual void Update()
    {
        if (LifeTime > 0f)
        {
            _alive += Time.deltaTime;
            if (_alive >= LifeTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
