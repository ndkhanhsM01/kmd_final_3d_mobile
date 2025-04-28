using System;
using UnityEngine;

[System.Serializable]
public class RewardInGame
{
    [SerializeField] private int amount;        public int Amount => amount;
    [SerializeField] private SOReward config;   public SOReward Config => config;

    public static Action<RewardInGame> OnReceiveNew;
    public void Receive()
    {
        config.Receive(amount);
        OnReceiveNew?.Invoke(this);
    }
}