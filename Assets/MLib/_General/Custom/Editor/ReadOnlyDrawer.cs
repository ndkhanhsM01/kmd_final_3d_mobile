// script copy from https://discussions.unity.com/t/read-only-fields/429693/12

using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MLib
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);

            //
            GUI.enabled = true;
        }
    }
}