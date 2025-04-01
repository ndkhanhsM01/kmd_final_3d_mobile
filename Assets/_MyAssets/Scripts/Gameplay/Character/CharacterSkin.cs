using Sirenix.OdinInspector;
using MLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class CharacterSkin : MonoBehaviour
{
    [SerializeField] private UnityEvent evtSkinChanged;
    [SerializeField] private GameObject[] allItems;
    [SerializeField] private SkinSet[] allSets;

    private void OnValidate()
    {
        for (int i = 0; i < allSets.Length; i++)
        {
            allSets[i].ValidateID(i);
        }
    }
    public void PutOnSkinSelected()
    {
        PutOn(SkinManager.Instance.IDSkinSelected);
    }

    [Button, PropertyOrder(-1)]
    public void PutOn(int idSet)
    {
        bool existSet = !allSets.IsOutOfRange(idSet);
        if (!existSet)
            return;

        TakeOffAll();
        allSets[idSet].Show();
        evtSkinChanged.Invoke();
    }

    public void TakeOffAll()
    {
        foreach (var item in allItems)
            item.SetActive(false);
    }

#if UNITY_EDITOR
    [Button("Get All Items"), PropertyOrder(-2)]
    private void Editor_GetAllItems()
    {
        allItems = transform.GetChildrenWithNameContains("AA").ToArray();
        EditorUtility.SetDirty(this);
    }

#endif
}