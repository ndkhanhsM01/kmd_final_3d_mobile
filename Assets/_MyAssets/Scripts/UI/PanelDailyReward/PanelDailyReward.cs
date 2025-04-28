
using UnityEngine;
using MLib;
using System;
using UnityEngine.UI;

public class PanelDailyReward: MPanel
{
    [SerializeField] private Button btnClose;
    [SerializeField] private SODailyRewardData savedData;
    [SerializeField] private SODailyRewardOrdered config;
    [SerializeField] private UIDailyRewardCell[] allCells;

    private void OnEnable()
    {
        btnClose.AddListener(OnClick_Close);
    }
    private void OnDisable()
    {
        btnClose.RemoveListener(OnClick_Close);
    }

    public override void Show(Action onFinish)
    {
        base.Show(onFinish);

        Reload();
    }

    private void Reload()
    {
        int streak = savedData.GetStreak();

        for (int day = 0; day < config.Rewards.Length; day++)
        {
            var reward = config.Rewards[day];
            var cell = allCells[day];
            bool claimed = savedData.CheckDayClaimed(day);
            bool claimable = day < streak && !claimed;
            cell.Setup(day, claimed, claimable, reward);
        }
    }

    private void OnClick_Close()
    {
        Hide();
    }
}