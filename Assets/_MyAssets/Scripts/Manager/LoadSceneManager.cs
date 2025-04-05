

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
        AudioManager.Instance.StopMusic();
        LoadSceneByAsset(sceneAsset_Home, true);
    }
    public void Load_Gameplay()
    {
        LoadSceneByAsset(sceneAsset_Gameplay, true);
    }
    public void Load_Common()
    {
        LoadSceneByAsset(sceneAsset_Common, false);
    }
    public void ReloadCurScene()
    {
        if (!curSceneAsset)
        {
            Debug.LogError("Curent scene asset is null!!");
            return;
        }

        LoadSceneByAsset(curSceneAsset, true);
    }

    public void LoadSceneByAsset(SOSceneAsset asset, bool isDestroyCurScene)
    {
#if UNITY_EDITOR
        sceneManager.Register_OnLoadDone(() => { Debug.Log("Load success: " + asset.name); });
#endif

        sceneManager.Register_OnLoadDone(asset.ReadyChannel.Raise);
        sceneManager.LoadScene(asset, isDestroyCurScene);
        curSceneAsset = asset;
    }
}