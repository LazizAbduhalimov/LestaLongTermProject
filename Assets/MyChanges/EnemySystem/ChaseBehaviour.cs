using PoolSystem.Alternative;
using System.Collections;
using UnityEngine;

public interface IEnemyBehaviour
{
    public void Tick();
    public void TickFixedUpdate();

    public void OnHit(GameObject agressor);
}

public class ChaseBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _target;

    [SerializeField] private float _damageZone;

    [SerializeField] private float _zoneCalmdown;
    [SerializeField] private bool _canAtack;

    [SerializeField] private PoolContainer _dropPoolData;


    private Rigidbody rb;

    public void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (_target == null) _target = GameObject.FindWithTag("Player").transform;
        // if (_dropPoolData == null) _dropPoolData = GameObject.FindWithTag("PlasmaContainer").GetComponent<PoolContainer>(); // такое нам не надо пж

        _canAtack = true;
        rb.freezeRotation = true;
    }
    public void Tick()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance < _damageZone && _canAtack)
        {
            Debug.Log("Hit logic");
            StartCoroutine(calmdown());
        }
    }

    public void TickFixedUpdate()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Vector3 targetPos = rb.position + direction * _speed * Time.fixedDeltaTime;

        rb.MovePosition(targetPos);

        Quaternion targetRot = Quaternion.LookRotation(direction);
        Quaternion smoothRot = Quaternion.Slerp(rb.rotation, targetRot, _rotateSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(smoothRot);
    }

    IEnumerator calmdown()
    {
        _canAtack = false;
        yield return new WaitForSeconds(_zoneCalmdown);
        _canAtack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.indianRed;
        Gizmos.DrawWireSphere(transform.position, _damageZone);
    }


    private void OnDeath()
    {
        if (_dropPoolData == null) return;
        var drop = _dropPoolData.Pool.GetFreeElement();
        drop.transform.position = transform.position;

        gameObject.SetActive(false);
    }
    public void OnHit(GameObject agressor)
    {
        OnDeath();
    }
}
