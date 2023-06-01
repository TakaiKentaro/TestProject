#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class HierarchyComponent
{
    private static readonly Color disabledColor = new Color(1f, 1f, 1f, 0.5f);

    private const int WIDTH = 16;
    private const int HEIGHT = 16;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go == null)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = pos.xMax - WIDTH;
        pos.width = WIDTH;
        pos.height = HEIGHT;

        IEnumerable<Component> cm = go
            .GetComponents<Component>()
            .Where(component => component != null)
            .Where(component => !(component is Transform))
            .Reverse();

        Event currentEvent = Event.current;

        foreach (var component in cm)
        {
            Texture image = AssetPreview.GetMiniThumbnail(component);

            if (image == null && component is MonoBehaviour)
            {
                var monoScript = MonoScript.FromMonoBehaviour(component as MonoBehaviour);
                var path = AssetDatabase.GetAssetPath(monoScript);
                image = AssetDatabase.GetCachedIcon(path);
            }

            if (image == null)
            {
                continue;
            }

            if (currentEvent.type == EventType.MouseDown && pos.Contains(currentEvent.mousePosition))
            {
                component.SetEnabled(!component.IsEnabled());
            }

            var color = GUI.color;
            GUI.color = component.IsEnabled() ? Color.white : disabledColor;
            GUI.DrawTexture(pos, image, ScaleMode.ScaleToFit);
            GUI.color = color;
            pos.x -= pos.width;
        }
    }

    public static bool IsEnabled(this Component component)
    {
        if (component == null)
        {
            return true;
        }

        var type = component.GetType();
        var property = type.GetProperty("enabled", typeof(bool));

        if (property == null)
        {
            return true;
        }

        return (bool)property.GetValue(component, null);
    }

    public static void SetEnabled(this Component component, bool isEnabled)
    {
        if (component == null)
        {
            return;
        }

        var type = component.GetType();
        var property = type.GetProperty("enabled", typeof(bool));

        if (property == null)
        {
            return;
        }

        property.SetValue(component, isEnabled, null);
    }
}

#endif
