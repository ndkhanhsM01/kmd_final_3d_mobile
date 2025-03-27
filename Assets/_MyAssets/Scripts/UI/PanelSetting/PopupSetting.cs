
using MLib;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting: MPopup
{
    [SerializeField] private Button btnHome;

    [SerializeField] private UISliderRaiseValue musicSlider;
    [SerializeField] private UISliderRaiseValue soundSlider;

    private LocalData localData => DataManager.LocalData;
    protected override void OnEnable()
    {
        base.OnEnable();
        btnHome.AddListener(OnClick_Home);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        btnHome.RemoveListener(OnClick_Home);
    }
    public void Setup()
    {
        musicSlider.SetValue(localData.VolumeMusic);
        soundSlider.SetValue(localData.VolumeSound);
    }
    private void OnClick_Home()
    {
        LoadSceneManager.Instance.Load_Home();
    }

}