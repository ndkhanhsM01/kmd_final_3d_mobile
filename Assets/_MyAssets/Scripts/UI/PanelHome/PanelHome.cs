using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelHome : MPanel 
{
    [Header("Buttons")]
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnSetting;

    [Header("Others")]
    [SerializeField] private SOVoidEventChannel sceneLoadedChannel;
    [SerializeField] private SOLevelsOrder levelOrder;
    [SerializeField] private Transform cellLevelsHolder;
    [SerializeField] private LevelCell levelCellPrefab;

    private void OnEnable()
    {
        sceneLoadedChannel.Register(OnSceneLoaded);
        btnContinue.AddListener(OnClick_Continue);
        btnReplay.AddListener(OnClick_Replay);
        btnSetting.AddListener(OnClick_Setting);
    }
    private void OnDisable()
    {
        sceneLoadedChannel.Unregister(OnSceneLoaded);
        btnContinue.RemoveListener(OnClick_Continue);
        btnReplay.RemoveListener(OnClick_Replay);
        btnSetting.RemoveListener(OnClick_Setting);
    }
    private void OnSceneLoaded()
    {
        SetupLevels();
    }
    private void OnClick_Continue()
    {
        GameManager.Instance.EnterGame();
    }
    private void OnClick_Replay()
    {
        MUIManager.Instance.ShowPanel<PanelConfirmReplay>();
    }
    private void OnClick_Setting()
    {
        MUIManager.Instance.ShowPanel<PanelSetting>();
    }

    public void SetupLevels()
    {
        for (int i = 0; i < levelOrder.TotalLevels; i++)
        {
            LevelCell cell = Instantiate(levelCellPrefab, cellLevelsHolder);
            cell.Setup(i);
        }
    }
}
