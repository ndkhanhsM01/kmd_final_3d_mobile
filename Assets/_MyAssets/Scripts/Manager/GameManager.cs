using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLib;

public class GameManager : MSingleton<GameManager> 
{
    protected override void Awake()
    {
        base.Awake();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    public void EnterGame()
    {
        LoadSceneManager.Instance.Load_Gameplay();
    }
}
