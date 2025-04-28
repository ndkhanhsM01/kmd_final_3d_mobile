using UnityEngine;

[CreateAssetMenu(fileName = "DailyRewardOrdered", menuName = "LiveOps/DailyReward/Reward Ordered")]
public class SODailyRewardOrdered : ScriptableObject
{
    [SerializeField] private RewardInGame[] rewards;

    public RewardInGame[] Rewards => rewards;

    public RewardInGame GetReward(int day)
    {
        return rewards[day];
    }
}
