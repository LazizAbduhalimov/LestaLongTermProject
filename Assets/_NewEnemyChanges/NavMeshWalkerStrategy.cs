using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu( menuName = "Scriptable Objects/StrategySO/NavMeshWalker")]
public class NavMeshWalkerStrategy : StrategySO
{
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotateSpeed;


    [SerializeField, Tooltip("At what distance from this target he will stop")] private float _stoppingDistance;

    [SerializeField, Tooltip("How often AI recalculates target position")] private float _updateTime;


    private NavMeshAgent _agent;
    private Transform _target;
    public override void Init(GameObject owner)
    {
        base.Init(owner);

        _agent = owner.GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("ChaseStrategy: NavMeshAgent missing", owner);
        }

        _target = FindAnyObjectByType<PlayerUnit>().GetComponent<Transform>();
        if (_target == null)
        {
            Debug.LogError("Game over", this);
        }

        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _agent.speed = _speed;
        _agent.acceleration = _acceleration;
        _agent.angularSpeed = _rotateSpeed;
        _agent.stoppingDistance = _stoppingDistance;

    }

    public override IEnumerator AI()
    {
        if (_agent == null)
            yield break;

        while (true)
        {
            _agent.SetDestination(_target.position);
            yield return new WaitForSeconds(_updateTime);
        }
    }
}
