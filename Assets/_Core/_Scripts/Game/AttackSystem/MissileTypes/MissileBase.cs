using System.Collections;
using UnityEngine;

public abstract class MissileBase : MonoBehaviour
{
    public float Speed = 10f;

    private TrailRenderer[] _trailRenderers;

    protected virtual void Awake()
    {
        _trailRenderers = GetComponentsInChildren<TrailRenderer>();
    }

    public virtual void Init(float speed, float lifeTime = 5f)
    {
        Speed = speed;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(ClearTrailNextFrame());
    }

    private IEnumerator ClearTrailNextFrame()
    {
        // очищаем трейл, чтобы не было артефактов от предыдущего использования. На всякий случай 2 раза
        if (_trailRenderers != null)
        {
            foreach (var trailRenderer in _trailRenderers)
            {
                trailRenderer.Clear();
            }
            yield return null;
            foreach (var trailRenderer in _trailRenderers)
            {
                trailRenderer.Clear();
            }
        }
    }
}
