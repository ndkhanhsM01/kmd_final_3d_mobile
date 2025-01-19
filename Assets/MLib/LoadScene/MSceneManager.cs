using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MLib
{
    public class MSceneManager : MSingleton<MSceneManager>
    {
        [SerializeField] [Range(0.1f, 0.9f)] private float percentAccept = 0.85f;

        [SerializeField] private SceneTransition transition;

        private Action onLoadStart;
        private Action onLoadDone;
        private Action<float> onProgressChanged;
        public void LoadScene(SOSceneAsset sceneAsset, bool destroyCurrentScene = true)
        {
            StartCoroutine(CR_LoadScene(sceneAsset, destroyCurrentScene));
        }

        private IEnumerator CR_LoadScene(SOSceneAsset sceneAsset, bool destroyCurrentScene)
        {
            transition.DoIn();
            yield return new WaitUntil(() => transition.IsDoneIn);

            #region unload scene
            string oldScene = SceneManager.GetActiveScene().name;
            if (destroyCurrentScene)
            {
                var unloadAsync = SceneManager.UnloadSceneAsync(oldScene);
                while (!unloadAsync.isDone) yield return null;
            }
            #endregion


            #region load scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneAsset.Index, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;

            onLoadStart?.Invoke();
            while (!asyncLoad.isDone)
            {
                onProgressChanged?.Invoke(asyncLoad.progress);
                if (asyncLoad.progress >= percentAccept && !asyncLoad.allowSceneActivation)
                {

                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneAsset.Index));
            #endregion

            int frameCount = 2;
            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }
            transition.DoOut();
            onLoadDone?.Invoke();
            ClearCallback();
        }

        public void Register_OnStart(Action callback)
        {
            onLoadStart += callback;
        }

        public void Register_OnLoadDone(Action callback)
        {
            onLoadDone += callback;
        }
        public void Register_OnProgressChanged(Action<float> callback)
        {
            onProgressChanged += callback;
        }
        public void ClearCallback()
        {
            onLoadStart = null;
            onLoadDone = null;
            onProgressChanged = null;
        }
    }
}
