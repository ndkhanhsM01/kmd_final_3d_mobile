using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLib.Examples
{
    public class DemoLoadScene : MonoBehaviour
    {
        public void Load()
        {
            MSceneManager.Instance.OnLoadStart += OnLoadStart;
            MSceneManager.Instance.OnProgressChanged += OnProgressChanged;
            MSceneManager.Instance.OnLoadDone += OnLoadDone;
            //MSceneManager.Instance.LoadScene(ConstraintSceneName.Demo1);
        }

        private void OnLoadStart()
        {
            Debug.Log("Load start");
        }
        private void OnProgressChanged(float progress)
        {
            Debug.Log($"Loading: {progress}");
        }
        private void OnLoadDone()
        {
            Debug.Log("Load done");
        }
    }
}
