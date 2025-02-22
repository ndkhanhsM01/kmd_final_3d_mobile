
using MLib;
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting: MPanel
{
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    private void Start()
    {
        MAudioManager.Instance.PlayMusic(MSoundType.Background);

        sliderMusic.onValueChanged.AddListener(OnMusicValueChange);
        sliderSFX.onValueChanged.AddListener(OnSFXValueChange);
    }
    public void OnClickSound()
    {
        MAudioManager.Instance.PlaySFX(MSoundType.ClickButton);
    }
    public void OnWinSound()
    {
        MAudioManager.Instance.PlaySFX(MSoundType.Win);
    }
    public void OnMusicValueChange(float value)
    {
        MAudioManager.Instance.SetVolumeMusic(value);
    }
    public void OnSFXValueChange(float value)
    {
        MAudioManager.Instance.SetVolumeSFX(value);
    }
}