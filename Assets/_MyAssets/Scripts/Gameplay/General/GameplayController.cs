using MLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MSingleton<GameplayController>
{
    [SerializeField] private InputHandler input;
    [SerializeField] private LevelLoader levelLoader;

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
}
