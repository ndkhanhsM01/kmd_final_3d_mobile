using MLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MSingleton<GameplayController>
{
    [SerializeField] private InputHandler input;
    [SerializeField] private LevelLoader levelLoader;

    [Space(20f)]
    [Header("Event channels")]
    [SerializeField] private SOVoidEventChannel channelStart;
    [SerializeField] private SOVoidEventChannel channelWin;
    [SerializeField] private SOVoidEventChannel channelLose;

    private Level curLevel;
    private MainCharacter mc;
    public MainCharacter MC => mc;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        curLevel = levelLoader.LoadLevel();

        mc = curLevel.MC;
    }

    public void StartPlay()
    {
        channelStart.Raise();
    }
    public void WinLevel()
    {
        channelWin.Raise();
    }
    public void LoseLevel()
    {
        channelLose.Raise();
    }
}
