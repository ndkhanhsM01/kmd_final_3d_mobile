using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelHome : MPanel 
{
    [Header("Buttons")]
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnSkin;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnDailyReward;

    [Header("Popup")]
    [SerializeField] private PopupConfirmReplay popupConfirmReplay;
    [SerializeField] private PopupSetting popupSetting;
    [SerializeField] private PopupLevel popupLevel;

    private void OnEnable()
    {
        btnContinue.AddListener(OnClick_Continue);
        btnReplay.AddListener(OnClick_Replay);
        btnSkin.AddListener(OnClick_Skin);
        btnSetting.AddListener(OnClick_Setting);
        btnDailyReward.AddListener(OnClick_DailyReward);
    }
    private void OnDisable()
    {
        btnContinue.RemoveListener(OnClick_Continue);
        btnReplay.RemoveListener(OnClick_Replay);
        btnSkin.RemoveListener(OnClick_Skin);
        btnSetting.RemoveListener(OnClick_Setting);
        btnDailyReward.RemoveListener(OnClick_DailyReward);
    }
    private void OnClick_Continue()
    {
        popupLevel.Show();
    }
    private void OnClick_Replay()
    {
        popupConfirmReplay.Show();
    }
    private void OnClick_Skin()
    {
        MUIManager.Instance.ShowPanel<PanelSkin>();
        this.Hide();
    }
    private void OnClick_Setting()
    {
        popupSetting.Show();
    }
    private void OnClick_DailyReward()
    {
        MUIManager.Instance.ShowPanel<PanelDailyReward>();
    }
}
