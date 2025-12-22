using UnityEditor;
using UnityEngine;

/// <summary>
/// Рисует кнопку "Properties" рядом с SO и Prefab'ами в Project window
/// </summary>
[InitializeOnLoad]
public static class ProjectWindowQuickAccess
{
    private static readonly Color ButtonColor = new Color(0.3f, 0.6f, 1f, 0.8f);
    private static readonly Vector2 ButtonSize = new Vector2(50f, 16f);
    
    static ProjectWindowQuickAccess()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
    }

    private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
    {
        // Получаем путь к ассету
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
        if (string.IsNullOrEmpty(assetPath))
            return;

        // Проверяем, является ли это SO или Prefab
        Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
        if (asset == null)
            return;

        bool isScriptableObject = asset is ScriptableObject;
        bool isPrefab = assetPath.EndsWith(".prefab");

        if (!isScriptableObject && !isPrefab)
            return;

        // Позиционируем кнопку справа
        Rect buttonRect = new Rect(
            selectionRect.xMax - ButtonSize.x - 4f,
            selectionRect.y + (selectionRect.height - ButtonSize.y) * 0.5f,
            ButtonSize.x,
            ButtonSize.y
        );

        // Рисуем фон кнопки
        Color previousColor = GUI.backgroundColor;
        GUI.backgroundColor = ButtonColor;
        
        if (GUI.Button(buttonRect, "Props", EditorStyles.miniButton))
        {
            // Открываем Properties (Inspector)
            OpenProperties(asset);
            Event.current.Use();
        }
        
        GUI.backgroundColor = previousColor;
    }

    private static void OpenProperties(Object asset)
    {
        // Открываем отдельное окно Properties (не меняет текущий Inspector)
        EditorUtility.OpenPropertyEditor(asset);
        
        // Пингуем объект для визуального выделения
        EditorGUIUtility.PingObject(asset);
    }
}
