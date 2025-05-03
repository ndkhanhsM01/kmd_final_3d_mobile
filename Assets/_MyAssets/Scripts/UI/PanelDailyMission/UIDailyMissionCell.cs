
using MLib;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDailyMissionCell: MonoBehaviour
{
    [SerializeField] private TMP_Text tmpTitle;
    [SerializeField] private TMP_Text tmpDescription;
    [SerializeField] private TMP_Text tmpCurrent;
    [SerializeField] private Image imgFillProgress;
    [SerializeField] private UIReward uiReward;

    [SerializeField] private GameObject goUnfinish;
    [SerializeField] private GameObject goClaimed;
    [SerializeField] private Button btnClaim;

    public SOMission Data { get; private set; }

    private void OnEnable()
    {
        btnClaim.AddListener(OnClick_Claim);
    }
    private void OnDisable()
    {
        btnClaim.RemoveListener(OnClick_Claim);
    }

    public void Setup(SOMission mission)
    {
        Data = mission;
        Reload();
    }

    public void Reload()
    {
        tmpTitle.text = Data.GetTitle();
        tmpDescription.text = Data.GetDescription();
        tmpCurrent.text = $"{Data.Current.ToString("00")}/{Data.Require.ToString("00")}";
        imgFillProgress.fillAmount = (float)Data.Current / Data.Require;

        goUnfinish.SetActive(!Data.IsReached);
        goClaimed.SetActive(Data.IsReached && Data.Claimed);
        btnClaim.SetActive(Data.IsReached && !Data.Claimed);

        uiReward.Setup(Data.Rewards[0]);
    }

    private void OnClick_Claim()
    {
        Data.ClaimRewards();
        btnClaim.SetActive(false);
        goUnfinish.SetActive(false);
        goClaimed.SetActive(true);
    }
}