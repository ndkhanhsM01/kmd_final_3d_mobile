using MLib;
using UnityEngine;

public class CommonInitalizer : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
        MCommonSceneLoader.LoadCommonScene();
#endif
    }
}