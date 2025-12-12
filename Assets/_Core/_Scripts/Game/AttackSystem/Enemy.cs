using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    private Color _originalColor;

    public void Awake()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        _originalColor = MeshRenderer.material.color;        
    }

    public void ActivateHighlight()
    {
        MeshRenderer.material.color = Color.red;
    }

    public void DeactivateHighlight()
    {
        MeshRenderer.material.color = _originalColor;
    }
}