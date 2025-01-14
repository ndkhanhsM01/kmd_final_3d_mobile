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

        private Action onLoadStart;
        private Action onLoadDone;
        private Action<float> onProgressChanged;
        public void LoadScene(int index, bool destroyCurrentScene = true)
        {
            StartCoroutine(CR_LoadScene(index, destroyCurrentScene));
        }

        private IEnumerator CR_LoadScene(int index, bool destroyCurrentScene)
        {
            string oldScene = SceneManager.GetActiveScene().name;
            if (destroyCurrentScene)
            {
                var unloadAsync = SceneManager.UnloadSceneAsync(oldScene);
                while (!unloadAsync.isDone) yield return null;
            }
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

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

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(index));

            int frameCount = 2;
            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }

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
