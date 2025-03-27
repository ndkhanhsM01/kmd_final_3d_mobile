using UnityEngine;
using System;
using Sirenix.OdinInspector;

[System.Serializable]
public struct SkinSet
{
    [SerializeField, ReadOnly] private int id;
    [SerializeField] private GameObject[] items;

    public int ID => id;

    public void ValidateID(int newID)
    {
        id = newID;
    }
    public void Show()
    {
        foreach (var item in items)
        {
            item.SetActive(true);
        }
    }
    public void Hide()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }
    }
}