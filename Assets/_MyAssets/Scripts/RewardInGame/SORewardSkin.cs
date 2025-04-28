using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Reward_Skin", menuName = "Reward/Skin")]
public class SORewardSkin : SOReward
{
    [SerializeField] private SOSkinData skinData;

    public override Sprite Preview => skinData.SprPreview;
    protected override void OnReceive(int amount)
    {
        SkinManager.Instance.UnlockSkin(skinData.IDReference);
    }
}