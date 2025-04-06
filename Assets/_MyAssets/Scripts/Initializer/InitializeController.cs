
using MLib;
using UnityEngine;

public class InitializeController: MonoBehaviour
{
    [SerializeField] private SOVoidEventChannel sceneReadyChannel;

    private void OnEnable()
    {
        sceneReadyChannel.Register(OnSceneLoaded);
    }
    private void OnDisable()
    {
        sceneReadyChannel.Unregister(OnSceneLoaded);
    }

    private void OnSceneLoaded()
    {
        if (DataManager.LocalData.IsFirsTimePlay)
        {
            GameManager.Instance.EnterGame();
            DataManager.LocalData.IsFirsTimePlay = false;
        }
        else
            LoadSceneManager.Instance.Load_Home();
    }
}