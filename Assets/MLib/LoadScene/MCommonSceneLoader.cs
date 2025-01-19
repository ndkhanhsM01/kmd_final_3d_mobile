#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

namespace MLib
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class MCommonSceneLoader
    {
        public static void LoadCommonScene()
        {
            var commonScene = SceneManager.GetSceneByName(SceneNameConst.Common);
            if (!commonScene.isLoaded)
            {
                var asyncCommon = SceneManager.LoadSceneAsync(SceneNameConst.Common, LoadSceneMode.Additive);

            }
        }
#if UNITY_EDITOR
        static MCommonSceneLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                LoadCommonScene();
            }
        }
#endif
    }
}
