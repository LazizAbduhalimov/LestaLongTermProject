using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SomeTargetController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _enemyLayer;

    private List<ITargetSelector> _selectors = new List<ITargetSelector>();
    private List<ITargetDetector> _detectors = new List<ITargetDetector>();

    private ITargetSelector _actualSelector;
    private ITargetDetector _actualDetector;

    private TargetSystem _targetSystem;



    private void Start()
    {
        _selectors.Add(new ClosestEnemySelector());

        _detectors.Add(new LayerOverlapDetector(50, _enemyLayer));
        _detectors.Add(new GenericOverlapDetector<Transform>(50));

        _actualSelector = _selectors[0];
        _actualDetector = _detectors[0];

        _targetSystem = new TargetSystem(_actualDetector, _actualSelector);


    }

    private void Update()
    {
        _target = GetTarget();
    }
    public Transform GetTarget()
    {
        return _targetSystem.GetTarget(gameObject.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blueViolet;

        Gizmos.DrawWireSphere(_target == null ? transform.position : _target.position, 5);
    }
}
