using System;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct RangeFloat
{
    public float min;
    public float max;

    public float GetRandomValue()
    {
        return Random.Range(min, max);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RangeFloat))]
public class RangeFloatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //float labelWidth = EditorGUIUtility.labelWidth;
        float labelWidth = EditorGUIUtility.labelWidth;
        //float fieldWidth = (position.width - labelWidth) / 2 - 30;
        float fieldWidth = 90;

        Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
        EditorGUI.LabelField(labelRect, label);

        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        Rect minLabelRect = new Rect(position.x + labelWidth, position.y, 70, position.height);
        EditorGUI.LabelField(minLabelRect, "Min");

        Rect minRect = new Rect(minLabelRect.x + minLabelRect.width + 5, position.y, fieldWidth, position.height);
        minProp.floatValue = EditorGUI.FloatField(minRect, GUIContent.none, minProp.floatValue);

        Rect maxLabelRect = new Rect(minRect.x + fieldWidth + 10, position.y, 70, position.height);
        EditorGUI.LabelField(maxLabelRect, "Max");

        Rect maxRect = new Rect(maxLabelRect.x + maxLabelRect.width + 5, position.y, fieldWidth, position.height);
        maxProp.floatValue = EditorGUI.FloatField(maxRect, GUIContent.none, maxProp.floatValue);

        EditorGUI.EndProperty();
    }
}
#endif
