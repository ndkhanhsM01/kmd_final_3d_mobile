using Cysharp.Threading.Tasks;
using MLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MSingleton<GameplayController>
{
    [SerializeField] private InputHandler input;
    [SerializeField] private LevelLoader levelLoader;

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

    private Level curLevel;
    private MainCharacter mc;
    public InputHandler Input => input;
    public MainCharacter MC => mc;
    public Level CurLevel => curLevel;
    public int TotalHostage => curLevel.Hostages.Length;
    public int CountHostageFreedom {  get; private set; }

    private void OnEnable()
    {
        LevelLoader.OnNewLevelLoaded += OnNewLevelLoaded;
        Hostage.OnRelease += OnSaveNewHostage;
    }
    private void OnDisable()
    {
        LevelLoader.OnNewLevelLoaded -= OnNewLevelLoaded;
        Hostage.OnRelease -= OnSaveNewHostage;
        
    }
    private void OnSaveNewHostage()
    {
        CountHostageFreedom++;
        var panelGameplay = MUIManager.Instance.GetPanel<PanelGameplay>();
        panelGameplay.SetHostageFreedom(CountHostageFreedom, TotalHostage);

        if (CountHostageFreedom == TotalHostage)
            WinLevel();
    }
    private void OnNewLevelLoaded(Level newLevel)
    {
        CountHostageFreedom = 0;
        var panelGameplay = MUIManager.Instance.GetPanel<PanelGameplay>();
        panelGameplay.SetHostageFreedom(CountHostageFreedom, TotalHostage);

        prisonKeyReference.Renew();
        prisonKeyReference.SetPrisonKeyPairs(newLevel.PrisonKeyPairs);
    }
    public void LoadNewLevel()
    {
#if UNITY_EDITOR
        if (!testingConfig.IsLoadLevel)
        {
            return;
        }
#endif
        curLevel = levelLoader.LoadLevel();
        LevelLoader.OnNewLevelLoaded?.Invoke(curLevel);
        mc = curLevel.MC;
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
