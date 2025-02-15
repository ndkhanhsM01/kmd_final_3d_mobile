using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[Serializable]
public struct RangeInt
{
    public int min;
    public int max;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RangeInt))]
public class RangeIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //float labelWidth = EditorGUIUtility.labelWidth;
        float labelWidth = EditorGUIUtility.labelWidth;
        //float fieldWidth = (position.width - labelWidth) / 2 - 30;
        float fieldWidth = 70;

        Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
        EditorGUI.LabelField(labelRect, label);

        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        Rect minLabelRect = new Rect(position.x + labelWidth, position.y, labelWidth, position.height);
        EditorGUI.LabelField(minLabelRect, "Min");

        Rect minRect = new Rect(minLabelRect.x + minLabelRect.width + 5, position.y, fieldWidth, position.height);
        minProp.intValue = EditorGUI.IntField(minRect, GUIContent.none, minProp.intValue);

        Rect maxLabelRect = new Rect(minRect.x + fieldWidth + 10, position.y, labelWidth, position.height);
        EditorGUI.LabelField(maxLabelRect, "Max");

        Rect maxRect = new Rect(maxLabelRect.x + maxLabelRect.width + 5, position.y, fieldWidth, position.height);
        maxProp.intValue = EditorGUI.IntField(maxRect, GUIContent.none, maxProp.intValue);

        EditorGUI.EndProperty();
    }
}
#endif
