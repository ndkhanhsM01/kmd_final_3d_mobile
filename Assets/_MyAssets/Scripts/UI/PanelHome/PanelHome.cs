using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelHome : MPanel 
{
    [Header("Buttons")]
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnSetting;

    [Header("Popup")]
    [SerializeField] private PopupConfirmReplay popupConfirmReplay;
    [SerializeField] private PopupSetting popupSetting;
    [SerializeField] private PopupLevel popupLevel;

    private void OnEnable()
    {
        btnContinue.AddListener(OnClick_Continue);
        btnReplay.AddListener(OnClick_Replay);
        btnSetting.AddListener(OnClick_Setting);
    }
    private void OnDisable()
    {
        btnContinue.RemoveListener(OnClick_Continue);
        btnReplay.RemoveListener(OnClick_Replay);
        btnSetting.RemoveListener(OnClick_Setting);
    }
    private void OnClick_Continue()
    {
        popupLevel.Show();
    }
    private void OnClick_Replay()
    {
        popupConfirmReplay.Show();
    }
    private void OnClick_Setting()
    {
        popupSetting.Show();
    }
}
