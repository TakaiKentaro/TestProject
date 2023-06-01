#if UNITY_EDITOR

using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchyを一行おきに色を変えて見やすくするエディタ拡張
/// </summary>
[InitializeOnLoad]
internal class HierarchyColorizer
{
    private const int RowHeight = 16;
    private const int OffsetY = -4;
    private static readonly Color Color = new Color(0, 0, 0, 0.3f);

    static HierarchyColorizer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += ColorizeHierarchyItem;
    }

    private static void ColorizeHierarchyItem(int instanceID, Rect rect)
    {
        int index = (int)(rect.y + OffsetY) / RowHeight;

        if (index % 2 == 0)
        {
            return;
        }

        float xMax = rect.xMax;

        rect.x = GetIndentation();
        rect.xMax = xMax + GetExtraWidth();

        EditorGUI.DrawRect(rect, Color);
    }

    private static float GetIndentation()
    {
        return 32; 
    }

    private static float GetExtraWidth()
    {
        return 16; 
    }
}

#endif