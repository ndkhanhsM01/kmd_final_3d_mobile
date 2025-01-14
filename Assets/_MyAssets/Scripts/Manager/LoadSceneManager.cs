

using MLib;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager: MSingleton<LoadSceneManager>
{
    [SerializeField] private SOSceneAsset sceneAsset_Home;
    [SerializeField] private SOSceneAsset sceneAsset_Common;
    [SerializeField] private SOSceneAsset sceneAsset_Gameplay;

    private SOSceneAsset curSceneAsset;

    private MSceneManager sceneManager => MSceneManager.Instance;
    public void Load_Home()
    {
        LoadSceneByAsset(sceneAsset_Home);
    }
    public void Load_Gameplay()
    {
        LoadSceneByAsset(sceneAsset_Gameplay);
    }
    public void Load_Common()
    {
        LoadSceneByAsset(sceneAsset_Common);
    }
    public void ReloadCurScene()
    {
        if (!curSceneAsset)
        {
            Debug.LogError("Curent scene asset is null!!");
            return;
        }

        LoadSceneByAsset(curSceneAsset);
    }

    private void LoadSceneByAsset(SOSceneAsset asset)
    {
#if UNITY_EDITOR
        sceneManager.Register_OnLoadDone(() => { Debug.Log("Load success: " + asset.name); });
#endif

        bool isDestroyCurScene = !curSceneAsset.IsCommon;
        sceneManager.Register_OnLoadDone(asset.ReadyChannel.Raise);
        sceneManager.LoadScene(asset.Index, isDestroyCurScene);
        curSceneAsset = asset;
    }
}