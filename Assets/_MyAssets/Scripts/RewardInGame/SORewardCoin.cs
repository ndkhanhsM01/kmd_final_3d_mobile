using MLib;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward_Coin", menuName = "Reward/Coin")]
public class SORewardCoin : SOReward
{
    protected override void OnReceive(int amount)
    {
        DataManager.Instance.Coin += amount;
    }
}