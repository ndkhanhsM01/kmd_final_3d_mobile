#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

namespace MLib
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class MCommonSceneLoader
    {
        static MCommonSceneLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                var currentScene = SceneManager.GetActiveScene();
                var commonScene = SceneManager.GetSceneByName(SceneNameConst.Common);
                Debug.Log("On Exit edit mode");
                Debug.Log($"{commonScene.name} scene state: " + commonScene.isLoaded);
                if (!commonScene.isLoaded)
                {
                    
                    var asyncCommon = SceneManager.LoadSceneAsync(SceneNameConst.Common, LoadSceneMode.Additive);
                    asyncCommon.completed += (x) =>
                    {
                        var asyncScene = SceneManager.LoadSceneAsync(currentScene.name, LoadSceneMode.Additive);
                        asyncScene.completed += (x) =>
                        {
                            SceneManager.UnloadSceneAsync(currentScene);
                        };
                    };
                }
            }
        }
    }
#endif
}
