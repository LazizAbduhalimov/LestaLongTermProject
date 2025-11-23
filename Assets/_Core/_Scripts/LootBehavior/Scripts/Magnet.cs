using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float _attractSpeed;
    [SerializeField] private float _magnetRadius;

    [SerializeField] private bool _enabledMagnet;


    private void Update()
    {
        if (!_enabledMagnet) return;

        var hits = Physics.OverlapSphere(transform.position, _magnetRadius);

        foreach (var hit in hits)
        {
            var a = hit.GetComponent<IAttractable>();

            a?.AttractTo(transform.position, _attractSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _magnetRadius);

    }
}
