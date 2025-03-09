using Cysharp.Threading.Tasks;
using MLib;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MSingleton<GameplayController>
{
    [SerializeField] private InputHandler input;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private PanelGameplay panelGameplay;

    [SerializeField] private SOPrisonKeyReference prisonKeyReference;

    [Space(20)]
    [Header("Debug")]
    [SerializeField] private EditorConfigSO testingConfig;

    [Space(20f)]
    [Header("Event channels")]
    [SerializeField] private SOVoidEventChannel channelStart;
    [SerializeField] private SOBoolEventChannel channelFreeze;
    [SerializeField] private SOVoidEventChannel channelWin;
    [SerializeField] private SOVoidEventChannel channelLose;

    [Space(20f)]
    [SerializeField, ReadOnly] private Level curLevel;
    private MainCharacter mc;
    public InputHandler Input => input;
    public MainCharacter MC => mc;
    public Level CurLevel => curLevel;
    public int TotalHostage => curLevel.Hostages.Length;
    public int CountHostageFreedom {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        LoadNewLevel();
    }
    private void OnEnable()
    {
        Hostage.OnRelease += OnSaveNewHostage;
    }
    private void OnDisable()
    {
        Hostage.OnRelease -= OnSaveNewHostage;
        
    }
    private void OnSaveNewHostage()
    {
        CountHostageFreedom++;
        panelGameplay.SetHostageFreedom(CountHostageFreedom, TotalHostage);

        if (CountHostageFreedom == TotalHostage)
            WinLevelDelay(1f);
    }
    public void LoadNewLevel()
    {
#if UNITY_EDITOR
        if (!testingConfig.IsLoadLevel)
        {
            return;
        }
#endif
        curLevel = FindFirstObjectByType<Level>();
        curLevel.BeginSetup();

        mc = curLevel.MC;
        CountHostageFreedom = 0;

        prisonKeyReference.Renew();
        prisonKeyReference.SetPrisonKeyPairs(curLevel.PrisonKeyPairs);

        panelGameplay.SetHostageFreedom(CountHostageFreedom, TotalHostage);
    }
    public void StartPlay()
    {
        channelStart.Raise();
        channelFreeze.Raise(false);
    }
    public void SetFreezeGame(bool status)
    {
        Debug.Log($"XX: Freeze <{status}>");
        channelFreeze.Raise(status);
    }
    public void WinLevel()
    {
        Debug.Log("XX: Win level");
        channelWin.Raise();
        SetFreezeGame(true);

        DataManager.Instance.LocalData.CurrentLevel++;
        MUIManager.Instance.ShowPanel<PanelGameWin>();
    }
    public async void WinLevelDelay(float delay)
    {
        Debug.Log("XX: Win level");
        channelWin.Raise();
        SetFreezeGame(true);
        DataManager.Instance.LocalData.CurrentLevel++;

        await UniTask.WaitForSeconds(delay);

        MUIManager.Instance.ShowPanel<PanelGameWin>();
    }
    public void LoseLevel()
    {
        Debug.Log("XX: Lose level");
        channelLose.Raise();
        SetFreezeGame(true);

        MUIManager.Instance.ShowPanel<PanelGameLose>();
    }
    public async void LoseLevelDelay(float delay)
    {
        Debug.Log("XX: Lose level");
        channelLose.Raise();
        SetFreezeGame(true);
        await UniTask.WaitForSeconds(delay);

        MUIManager.Instance.ShowPanel<PanelGameLose>();
    }
}
