using DG.Tweening;
using MLib;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CoinDrop : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private Transform model;
    [SerializeField] private UnityEvent evtPickup;
    public void Trigger(Transform source)
    {
        sharedCoin.Value++;
        evtPickup?.Invoke();
        DoAnimPickup();
    }

    private void DoAnimPickup()
    {
        model.DoAnimPickup(4f, () =>
        {
            gameObject.SetActive(false);
        });
    }
}
