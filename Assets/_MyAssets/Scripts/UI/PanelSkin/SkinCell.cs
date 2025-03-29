
using MLib;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using ReadOnly = Sirenix.OdinInspector.ReadOnlyAttribute;

public class SkinCell: MonoBehaviour
{
    [SerializeField, ReadOnly] private SOSkinData info;
    [SerializeField] private Image imgPreview;
    [SerializeField] private Button button;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject equiped;

    public SOSkinData Info => info;
    private void OnEnable()
    {
        button.AddListener(OnClick);
    }
    private void OnDisable()
    {
        button.RemoveListener(OnClick);
    }
    public void UpdateByData()
    {
        imgPreview.sprite = info.SprPreview;
        equiped.SetActive(info.IsChoosing);
        highlight.SetActive(false);
    }
    public void Setup(SOSkinData skinData)
    {
        info = skinData;
        UpdateByData();
    }
    public void SetEquiped(bool value)
    {
        equiped.SetActive(value);
    }
    public void Select()
    {
        highlight.SetActive(true);
        button.interactable = false;
    }
    public void Unselect()
    {
        highlight.SetActive(false);
        button.interactable = true;
    }
    private void OnClick()
    {
        Select();
        PanelSkin.OnClickSkinCell?.Invoke(this);
    }
}