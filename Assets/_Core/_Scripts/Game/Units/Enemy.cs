using UnityEngine;

public class Enemy : Unit
{
    public MeshRenderer MeshRenderer;
    private Color _originalColor;

    public void Awake()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        if (MeshRenderer != null)
            _originalColor = MeshRenderer.material.color;        
    }

    public void ActivateHighlight()
    {
        if (MeshRenderer != null)
            MeshRenderer.material.color = Color.red;
    }

    public void DeactivateHighlight()
    {
        if (MeshRenderer != null)
            MeshRenderer.material.color = _originalColor;
    }
}