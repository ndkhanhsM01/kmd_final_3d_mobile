
using UnityEngine;
using MLib;
using System;

public class PanelSetting: MPanel
{
    [SerializeField] private PopupSetting popup;


    public override void Show(Action onFinish)
    {
        base.Show(onFinish);
        popup.Show();
    }
    public override void Hide(Action onFinish)
    {
        base.Hide(onFinish);
        popup.Hide();
    }
}