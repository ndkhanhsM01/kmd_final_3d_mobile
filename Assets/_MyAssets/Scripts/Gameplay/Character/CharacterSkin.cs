
using MLib;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSkin : MonoBehaviour
{
    [SerializeField] private UnityEvent evtSkinChanged;
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
        foreach (var set in allSets)
            set.Hide();
    }
}