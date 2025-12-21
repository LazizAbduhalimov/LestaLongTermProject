using UnityEngine;

public class ShowColliderGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider != null && meshCollider.sharedMesh != null)
        {
            Gizmos.color = Color.green; // Or any color you prefer
            Gizmos.DrawWireMesh(meshCollider.sharedMesh, transform.position, transform.rotation, transform.localScale);
        }
    }
}