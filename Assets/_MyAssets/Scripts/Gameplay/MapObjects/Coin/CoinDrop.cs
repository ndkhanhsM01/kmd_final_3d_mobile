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
    public int ID => GetInstanceID();
    public bool IsCollected => DataManager.LocalData.CoinsCollected.Contains(ID);
    public void Start()
    {
        if (IsCollected)
            gameObject.SetActive(false);
    }
    public void Trigger(Transform source)
    {
        sharedCoin.Value++;
        evtPickup?.Invoke();
        DataManager.LocalData.CoinsCollected.Add(ID);
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
