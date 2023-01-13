using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarcyWindow : MonoBehaviour
{
    static CustomHierarcyWindow()
    {
        EditorApplication.hierarchyChanged += onWindowChanged;
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOmGUI;
    }

    static void onWindowChanged()
    {
        EditorApplication.RepaintHierarchyWindow();
    }

    static void HandleHierarchyWindowItemOmGUI(int inSelectionID, Rect inSelectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

        if (obj != null)
        {
            CustomHierarchyItem Label = obj.GetComponent<CustomHierarchyItem>();
            if (Label == null)
                return;

            if (Label != null)
            {
                #region Determine Styling

                Color BKCol =Label.BackgroundColor;
                Color TextCol =  Label.TextColor;
                FontStyle TextStyle = Label.FontStyle;

                #endregion

                #region Draw Background

                //Only draw background if background color is not completely transparent
                if (BKCol.a > 0f)
                {
                    EditorGUI.DrawRect(new Rect(inSelectionRect.x, inSelectionRect.y, inSelectionRect.width + 100, inSelectionRect.height), BKCol);

                    if (Label.Texture != null)
                        DrawIcon(inSelectionRect, Label.Texture, Label.IconSize);
                }

                #endregion

                DrawLabel(inSelectionRect, Label.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = TextCol },
                    fontStyle = TextStyle
                }, Label.IconSize);
            }
        }
    }

    private static void DrawIcon(Rect rect, Texture texture, Vector2 iconSize)
    {
        Rect r = new Rect(rect.x + 3, rect.y, iconSize.x, iconSize.y);
        GUI.DrawTexture(r, texture);
    }

    private static void DrawLabel(Rect rect, string label, GUIStyle style, Vector2 iconSice)
    {
        Rect r = new Rect(rect.x + iconSice.x + 10, rect.y, rect.width, rect.height);
        EditorGUI.LabelField(r, label, style);
    }
}
#endif
