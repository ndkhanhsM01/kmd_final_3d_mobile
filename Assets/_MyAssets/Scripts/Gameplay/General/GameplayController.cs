using Cysharp.Threading.Tasks;
using DG.Tweening;
using MLib;
using UnityEngine;

public class GameplayController : MSingleton<GameplayController>
{
    [SerializeField] private CostRevive costRevive;

    [Header("Others")]
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private SOIntVariable countFinishLevel;
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
    [SerializeField] private SOVoidEventChannel reviveChannel;

    [Space(20f)]
    [SerializeField, ReadOnly] private Level curLevel;
    public InputHandler Input => input;
    public Level CurLevel => curLevel;
    public int TotalHostage => curLevel.Hostages.Length;
    public int CountHostageFreedom {  get; private set; }
    public static MainCharacter MC { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        LoadNewLevel();
    }
    private void OnEnable()
    {
        Hostage.OnRelease += OnSaveNewHostage;
        reviveChannel.Register(OnRevive);
    }
    private void OnDisable()
    {
        Hostage.OnRelease -= OnSaveNewHostage;
        reviveChannel.Unregister(OnRevive);

        DOTween.KillAll();
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

        MC = curLevel.MC;
        CountHostageFreedom = 0;

        prisonKeyReference.Renew();
        prisonKeyReference.SetPrisonKeyPairs(curLevel.PrisonKeyPairs);

        panelGameplay.SetHostageFreedom(CountHostageFreedom, TotalHostage);

        costRevive.Init();
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

        DataManager.LocalData.CurrentLevel++;
        countFinishLevel.Value++;
        ShowUIWin();
    }
    public async void WinLevelDelay(float delay)
    {
        Debug.Log("XX: Win level");
        channelWin.Raise();
        SetFreezeGame(true);
        DataManager.LocalData.CurrentLevel++;
        countFinishLevel.Value++;

        await UniTask.WaitForSeconds(delay);

        ShowUIWin();
    }
    private void SetupLose()
    {
        Debug.Log("XX: Lose level");
        channelLose.Raise();
        SetFreezeGame(true);

    }
    public void LoseLevel()
    {
        SetupLose();
        ShowUILose();
    }
    public async void LoseLevelDelay(float delay)
    {
        SetupLose();
        await UniTask.WaitForSeconds(delay);

        ShowUILose();
    }

    private void ShowUILose()
    {
        MUIManager.Instance.HidePanel<PanelGameplay>();
        bool canRevive = costRevive.Value < sharedCoin.Value;
        if(canRevive)
            MUIManager.Instance.ShowPanel<PanelRevive>();
        else
            MUIManager.Instance.ShowPanel<PanelGameLose>();
    }

    private void OnRevive()
    {
        costRevive.Increase();
    }
    private void ShowUIWin()
    {
        MUIManager.Instance.HidePanel<PanelGameplay>();
        MUIManager.Instance.ShowPanel<PanelGameWin>();

    }
}
