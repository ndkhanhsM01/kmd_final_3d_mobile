
using UnityEngine;
using UnityEngine.UI;
using MLib;
using TMPro;

public class PanelNewReward: MPanel
{
    [SerializeField] private Image imgPreview;
    [SerializeField] private TMP_Text tmpAmount;
    [SerializeField] private Button buttonOk;

    private void OnEnable()
    {
        RewardInGame.OnReceiveNew += OnReceiveNewReward;
        buttonOk.AddListener(OnClick_Ok);
    }
    private void OnDisable()
    {
        RewardInGame.OnReceiveNew -= OnReceiveNewReward;
        buttonOk.RemoveListener(OnClick_Ok);
    }
    private void OnReceiveNewReward(RewardInGame reward)
    {
        imgPreview.sprite = reward.Config.Preview;
        tmpAmount.text = $"x{reward.Amount}";
        Show();
    }

    private void OnClick_Ok()
    {
        Hide();
    }
}