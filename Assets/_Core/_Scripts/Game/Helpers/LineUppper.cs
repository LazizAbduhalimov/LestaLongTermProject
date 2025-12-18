using Sirenix.OdinInspector;
using UnityEngine;

public class LineUppper : MonoBehaviour
{
    [Header("Line Settings")]
    public float Spacing = 2f;
    public int ObjectsPerLine = 5;
    public Vector3 LineDirection = Vector3.right;
    public Vector3 NextLineOffset = Vector3.forward;

    [Button("Line Up Children")]
    public void LineUpChildren()
    {
        int childCount = transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);
            
            int lineIndex = i / ObjectsPerLine;
            int posInLine = i % ObjectsPerLine;
            
            Vector3 position = LineDirection.normalized * (posInLine * Spacing) 
                             + NextLineOffset.normalized * (lineIndex * Spacing);
            
            child.localPosition = position;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (transform.childCount == 0) return;

        Gizmos.color = Color.yellow;
        int childCount = transform.childCount;
        
        for (int i = 0; i < childCount - 1; i++)
        {
            int lineIndex = i / ObjectsPerLine;
            int posInLine = i % ObjectsPerLine;
            int nextLineIndex = (i + 1) / ObjectsPerLine;
            int nextPosInLine = (i + 1) % ObjectsPerLine;
            
            Vector3 pos = transform.position + LineDirection.normalized * (posInLine * Spacing) 
                        + NextLineOffset.normalized * (lineIndex * Spacing);
            
            Vector3 nextPos = transform.position + LineDirection.normalized * (nextPosInLine * Spacing) 
                            + NextLineOffset.normalized * (nextLineIndex * Spacing);
            
            // Рисуем линию только между соседями на одной линии
            if (lineIndex == nextLineIndex)
            {
                Gizmos.DrawLine(pos, nextPos);
            }
        }
    }
#endif
}
