using System.Collections;
using PoolSystem.Alternative;
using UnityEngine;

public abstract class MissileBase : PoolObject
{
    [Header("Movement")]
    public float Speed = 10f;

    [Tooltip("Optional lifetime in seconds. 0 disables auto-destroy.")]
    public float LifeTime = 0f;
    private TrailRenderer _trailRenderer;

    private float _alive;

    public void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public virtual void Init(float speed, float lifeTime = 5f)
    {
        Speed = speed;
        LifeTime = lifeTime;
    }

    public virtual void OnEnable()
    {
        StartCoroutine(ClearTrailNextFrame());
        _alive = 0f;
    }

    private IEnumerator ClearTrailNextFrame()
    {
        _trailRenderer.Clear();
        yield return null;
        _trailRenderer.Clear();
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
