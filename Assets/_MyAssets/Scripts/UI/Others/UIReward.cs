
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIReward : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TMP_Text tmpAmount;
    public RewardInGame Data { get; private set; }

    public void Setup(RewardInGame data)
    {
        Data = data;
        imgIcon.sprite = Data.Config.Preview;
        tmpAmount.text = $"x{Data.Amount}";
    }

}