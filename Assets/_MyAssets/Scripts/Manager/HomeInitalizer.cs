using MLib;
using UnityEngine;

public class HomeInitalizer : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
        MCommonSceneLoader.LoadCommonScene();
#endif
    }
}