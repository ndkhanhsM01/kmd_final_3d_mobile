
using UnityEngine;
using MLib;
using UnityEngine.UI;
using TMPro;

public class PanelGameplay: MPanel
{
    [SerializeField] private TMP_Text tmpHostage;
    [SerializeField] private Button btnSetting;

    private void OnEnable()
    {
        btnSetting.AddListener(OnClick_Setting);
    }
    private void OnDisable()
    {
        btnSetting.RemoveListener(OnClick_Setting);
    }
    public void SetHostageFreedom(int amountFreedom, int total)
    {
        tmpHostage.text = $"{amountFreedom}/{total}";
    }

    private void OnClick_Setting()
    {
        MUIManager.Instance.ShowPanel<PanelSetting>();
    }
}