
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MLib
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class MUnityObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var targetObject = target as UnityEngine.Object;

            if (targetObject == null) return;

            MethodInfo[] methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                var attributes = (MButtonAttribute[])method.GetCustomAttributes(typeof(MButtonAttribute), true);
                if (attributes.Length > 0)
                {
                    string buttonLabel = attributes[0].ButtonLabel ?? method.Name;

                    if (GUILayout.Button(buttonLabel))
                    {
                        method.Invoke(targetObject, null);

                        if (targetObject is MonoBehaviour)
                        {
                            EditorUtility.SetDirty(targetObject);
                        }
                        else if (targetObject is ScriptableObject)
                        {
                            EditorUtility.SetDirty(targetObject);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
            }
        }

    }
}