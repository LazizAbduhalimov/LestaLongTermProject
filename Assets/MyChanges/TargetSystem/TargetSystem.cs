using System.Collections.Generic;
using UnityEngine;

public interface ITargetSelector
{
    Transform SelectTarget(List<Transform> targets, Transform owner);
}

public interface ITargetDetector
{
    List<Transform> DetectTargets(Transform owner);
}
public class TargetSystem 
{
    private ITargetDetector targetDetector;

    private ITargetSelector targetSelector;
    public TargetSystem(ITargetDetector detector, ITargetSelector selector)
    {
        targetDetector = detector;
        targetSelector = selector;
    }

    public Transform GetTarget(Transform owner)
    {
        var targets = targetDetector.DetectTargets(owner);
        return targetSelector.SelectTarget(targets, owner);
    }

    public void SetSelector(ITargetSelector selector)
    {
        targetSelector = selector;
    }

    public void SetDetector(ITargetDetector detector)
    {
        targetDetector = detector;
    }
}
