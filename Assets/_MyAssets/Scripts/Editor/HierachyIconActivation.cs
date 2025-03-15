
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class HierachyIconActivation
{
    static HierachyIconActivation()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierachyWindowItemOnGUI;
    }

    private static void OnHierachyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj == null)
        {
            return;
        }

        Rect rect = new Rect(selectionRect.x, selectionRect.y, 15f, selectionRect.height);
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0
            && rect.Contains(Event.current.mousePosition))
        {
            Undo.RecordObject(obj, "Changing active state of object");
            obj.SetActive(!obj.activeSelf);

            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }

            Event.current.Use();
        }
    }
}