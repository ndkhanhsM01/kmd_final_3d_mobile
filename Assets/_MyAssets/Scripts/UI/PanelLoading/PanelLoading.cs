using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelLoading: MPanel
{
    [SerializeField] private SOFloatEventChannel loadingChannel;
    [SerializeField] private Image imgFill;

    private void OnEnable()
    {
        loadingChannel.Register(OnLoading);
    }
    private void OnDisable()
    {
        loadingChannel.Unregister(OnLoading);
    }

    private void OnLoading(float value)
    {
        imgFill.fillAmount = value;
    }
}