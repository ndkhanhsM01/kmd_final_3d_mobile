
using MLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDailyRewardCell: MonoBehaviour
{
    [SerializeField] private SODailyRewardData savedData;
    [SerializeField] private TMP_Text tmpDay;
    [SerializeField] private TMP_Text tmpAmount;
    [SerializeField] private Image imgPreview;

    [SerializeField] private GameObject goClaimable;
    [SerializeField] private GameObject goClaimed;

    [SerializeField] private Button button;

    private int day;
    private RewardInGame reward;

    private void OnEnable()
    {
        button.AddListener(OnClick);
    }
    private void OnDisable()
    {
        button.RemoveListener(OnClick);
    }
    public void Setup(int day, bool claimed, bool claimable, RewardInGame reward)
    {
        this.day = day;
        this.reward = reward;

        tmpDay.text = $"Ngày {day+1}";
        tmpAmount.text = $"x{reward.Amount}";

        goClaimable.SetActive(claimable);
        goClaimed.SetActive(claimed);
        imgPreview.sprite = reward.Config.Preview;

        button.interactable = claimable;
    }

    private void OnClick()
    {
        Claim();
    }

    private void Claim()
    {
        reward.Receive();
        savedData.MarkDayClaimed(day);
        goClaimed.SetActive(true);
        goClaimable.SetActive(false);
        button.interactable = false;
    }
}