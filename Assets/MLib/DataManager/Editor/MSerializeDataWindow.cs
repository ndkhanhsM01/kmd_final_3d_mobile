
using UnityEditor;
using UnityEngine;

namespace MLib
{
    public class MSerializeDataWindow : EditorWindow
    {

        [MenuItem("MLib/Serialize Data")]
        public static void ShowExample()
        {
            MSerializeDataWindow wnd = GetWindow<MSerializeDataWindow>();
            wnd.titleContent = new GUIContent("Serialize Local Data");
        }
    }
}