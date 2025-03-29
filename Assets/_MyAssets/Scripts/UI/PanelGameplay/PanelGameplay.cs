
using UnityEngine;
using MLib;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PanelGameplay: MPanel
{
    [SerializeField] private TMP_Text tmpHostage;
    [SerializeField] private Button btnSetting;

    [Header("Key")]
    [SerializeField] private SOPrisonKeyReference keyReference;
    [SerializeField] private UIKeySlot keySlotPrefab;
    [SerializeField] private Transform keysHolder;

    private Queue<UIKeySlot> availableKeySlots;
    private Dictionary<PrisonKey, UIKeySlot> dictKeySlots;

    private void Awake()
    {
        availableKeySlots = new();
        dictKeySlots = new();
    }
    private void OnEnable()
    {
        btnSetting.AddListener(OnClick_Setting);
        keyReference.EvtNewKeyAdded += OnNewKeyAdded;
        keyReference.EvtKeyRemoved += OnKeyRemoved;
        keyReference.EvtKeyCleared += OnKeysCleared;
    }
    private void OnDisable()
    {
        btnSetting.RemoveListener(OnClick_Setting);
        keyReference.EvtNewKeyAdded -= OnNewKeyAdded;
        keyReference.EvtKeyRemoved -= OnKeyRemoved;
        keyReference.EvtKeyCleared -= OnKeysCleared;
    }
    private void OnSceneLoaded()
    {/*
        var gameController = GameplayController.Instance;
        SetHostageFreedom(gameController.CountHostageFreedom, gameController.TotalHostage);*/
    }
    public void SetHostageFreedom(int amountFreedom, int total)
    {
        tmpHostage.text = $"{amountFreedom}/{total}";
    }

    private void OnClick_Setting()
    {
        MUIManager.Instance.ShowPanel<PanelSetting>();
    }

    private void OnNewKeyAdded(PrisonKey newKey)
    {
        var slot = GetKeySlot();
        slot.Setup(newKey.GetColor());
        slot.Show();
        dictKeySlots.Add(newKey, slot);
    }

    private void OnKeyRemoved(PrisonKey key)
    {
        var slot = dictKeySlots[key];
        slot.Hide(() => ReturnSlot(slot));

        dictKeySlots.Remove(key);
    }

    private void OnKeysCleared()
    {
        if (dictKeySlots != null)
        {
            foreach (var slot in dictKeySlots.Values)
            {
                slot.SetActive(false);
            }
            dictKeySlots.Clear();
        }
    }

    private UIKeySlot GetKeySlot()
    {
        if(availableKeySlots.Count <= 0)
        {
            var clone = Instantiate(keySlotPrefab, keysHolder);
            //availableKeySlots.Enqueue(clone);
            return clone;
        }
        else
        {
            return availableKeySlots.Dequeue();
        }
    }
    private void ReturnSlot(UIKeySlot slot)
    {
        availableKeySlots.Enqueue(slot);
    }
}