using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour, ITargetDetector
{
    [SerializeField] private LayerMask targetMask;

    private List<Transform> targets = new List<Transform>();

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (!targets.Contains(other.transform))
                targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.transform))
            targets.Remove(other.transform);
    }

    public List<Transform> DetectTargets(Transform owner)
    {
        return new List<Transform>(targets);
    }
}
