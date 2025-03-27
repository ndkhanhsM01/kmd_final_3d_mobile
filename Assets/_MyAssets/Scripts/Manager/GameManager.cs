using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLib;

public class GameManager : MSingleton<GameManager>
{
    [SerializeField] private SOVoidEventChannel sceneLoadedChannel;
    [SerializeField] private SOLevelsOrder levelsOrder;

    protected override void Awake()
    {
        base.Awake();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        FakeSceneLoaded();
    }
    public void EnterGame()
    {
        //LoadSceneManager.Instance.Load_Gameplay();
        int levelReached = DataManager.LocalData.CurrentLevel;
        var sceneAsset = levelsOrder.GetLevelSceneAsset(levelReached);
        LoadSceneManager.Instance.LoadSceneByAsset(sceneAsset, true);
    }
    private void FakeSceneLoaded()
    {
        StartCoroutine(IE_FakeSceneLoaded());
    }

    private IEnumerator IE_FakeSceneLoaded()
    {
        byte count = 0;
        while (count < 2)
        {
            count++;
            yield return null;
        }

        sceneLoadedChannel.Raise();
    }

}
