
using UnityEngine;
using MLib;
using UnityEngine.UI;
using TMPro;

public class PanelGameplay: MPanel
{
    //[SerializeField] private SOVoidEventChannel sceneLoadedChannel;
    [SerializeField] private TMP_Text tmpHostage;
    [SerializeField] private Button btnSetting;

    private void OnEnable()
    {
        btnSetting.AddListener(OnClick_Setting);
        //sceneLoadedChannel.Register(OnSceneLoaded);
    }
    private void OnDisable()
    {
        btnSetting.RemoveListener(OnClick_Setting);
        //sceneLoadedChannel.Unregister(OnSceneLoaded);
    }
    private void OnSceneLoaded()
    {
        var gameController = GameplayController.Instance;
        SetHostageFreedom(gameController.CountHostageFreedom, gameController.TotalHostage);
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