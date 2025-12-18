using UnityEngine;

public class RaycastMissile : MissileBase
{
    [Header("Raycast")]
    public float MaxDistancePerStepMultiplier = 2f;
    public LayerMask HitMask = ~0;

    public GameObject HitEffect;

    protected void Update()
    {
        var stepDist = Speed * Time.deltaTime;
        var rayDist = Mathf.Max(stepDist * MaxDistancePerStepMultiplier, stepDist);
        var start = transform.position;
        var dir = transform.forward;

        if (Physics.Raycast(start, dir, out var hit, rayDist, HitMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = hit.point;
            OnHit(hit);
        }
        else
        {
            transform.position = start + dir * stepDist;
        }
    }

    private void OnHit(RaycastHit hit)
    {
        Debug.Log("Hit " + hit.collider.gameObject.name);
        if (HitEffect != null)
        {
            Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        Destroy(gameObject);
    }
}
