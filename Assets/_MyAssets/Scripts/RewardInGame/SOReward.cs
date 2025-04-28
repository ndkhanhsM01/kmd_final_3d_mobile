
using Sirenix.OdinInspector;
using System;
using UnityEngine;

//-> [CreateAssetMenu(fileName = "Reward_", menuName = "Reward/")]
public abstract class SOReward: ScriptableObject
{
    [SerializeField, PreviewField] protected Sprite preview;        public virtual Sprite Preview => preview;

    public void Receive(int amount)
    {
        OnReceive(amount);
    }
    protected abstract void OnReceive(int amount);
}