using UnityEngine;
using MLib;
using System.Collections.Generic;

public class SkinManager : MSingleton<SkinManager>
{
    [SerializeField] private SOSkinData[] allSkinsOrdered;
    

    private Dictionary<int, SOSkinData> dictSkins;

    public SOSkinData SkinSelected => dictSkins[localData.SkinSelected];
    public int IDSkinSelected => dictSkins[localData.SkinSelected].IDReference;
    public SOSkinData[] AllSkinsOrdered => allSkinsOrdered;
    private LocalData localData => DataManager.LocalData;

    protected override void Awake()
    {
        base.Awake();
        InitializeSkins();
    }
    private void InitializeSkins()
    {
        dictSkins = new();
        foreach (var skin in AllSkinsOrdered)
        {
            dictSkins.Add(skin.IDReference, skin);
        }
    }

    public void UpdateSkinSelected(int id)
    {
        localData.SkinSelected = id;
    }
    public void UnlockSkin(int id)
    {
        localData.SkinsUnlocked.Add(id);
    }
}