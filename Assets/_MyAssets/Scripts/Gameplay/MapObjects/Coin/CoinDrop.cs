using DG.Tweening;
using MLib;
using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ReadOnly = Sirenix.OdinInspector.ReadOnlyAttribute;

public class CoinDrop : MonoBehaviour, ITriggerable
{
    [SerializeField, ReadOnly] private int id = -1;
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private Transform model;
    [SerializeField] private UnityEvent evtPickup;

    private void OnEnable()
    {
        if (DataManager.LocalData != null)
        {
            bool isCollected = DataManager.LocalData.CoinsCollected.Contains(id);
            if(isCollected)
                gameObject.SetActive(false);

        }
    }
    public void Trigger(Transform source)
    {
        sharedCoin.Value++;
        evtPickup?.Invoke();
        DataManager.LocalData.CoinsCollected.Add(id);
        DoAnimPickup();
    }

    private void DoAnimPickup()
    {
        model.DoAnimPickup(4f, () =>
        {
            gameObject.SetActive(false);
        });
    }

#if UNITY_EDITOR
    [Button]
    public void ValidateID()
    {
        id = (int) TimeHelper.UnixTimeNow;
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
