using UnityEngine;
using MLib;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PanelSkin : MPanel
{
    [Header("Events")]
    [SerializeField] private SOVoidEventChannel showPanelChannel;
    [SerializeField] private SOVoidEventChannel hidePanelChannel;

    [Header("References")]
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private Transform cellHolder;
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSelect;
    [SerializeField] private UIBtnSpendResource btnBuy;
    [SerializeField] private TMP_Text tmpPriceCurrent;
    [SerializeField] private List<SkinCell> skinCells;

    public static Action<SkinCell> OnClickSkinCell;

    private bool initialized;
    private SkinCell cellSelected;
    private Dictionary<int, SkinCell> dictCell;

    private void Awake()
    {
        dictCell = new();
    }
    private void OnEnable()
    {
        btnBack.AddListener(OnClickHide);
        btnSelect.AddListener(OnClickSelect);
        btnBuy.AddListener(OnClickBuy);
        OnClickSkinCell += Callback_ClickSkinCell;
    }
    private void OnDisable()
    {
        btnBack.RemoveListener(OnClickHide);
        btnSelect.RemoveListener(OnClickSelect);
        btnBuy.RemoveListener(OnClickBuy);
        OnClickSkinCell -= Callback_ClickSkinCell;
    }
    public override void Show(Action onFinish)
    {
        base.Show(onFinish);
        showPanelChannel.Raise();

        if (!initialized)
        {
            initialized = true;
            SetupItems();
        }
        else
        {
            Reload();
        }

        FocusCellSelected();
    }
    public override void Hide(Action onFinish)
    {
        base.Hide(onFinish);
        hidePanelChannel.Raise();
    }
    public void Reload()
    {
        foreach (var cell in skinCells)
        {
            cell.UpdateByData();
            cell.Unselect();
        }
    }
    private void SetupItems()
    {
        var allSkins = GetAllSkins();

        for (int i = 0; i < allSkins.Length; i++)
        {
            bool enoughCells = i < skinCells.Count;
            if (!enoughCells)
                InitNewCell();

            var data = allSkins[i];
            var cell = skinCells[i];
            cell.Setup(data);
            dictCell.Add(data.IDReference, cell);
            cell.Unselect();
        }
    }
    private void FocusCellSelected()
    {
        var dataSelected = SkinManager.Instance.SkinSelected;
        cellSelected = dictCell[dataSelected.IDReference];
        cellSelected.Select();
        UpdateActiveButtons(dataSelected);
    }
    public void UpdateActiveButtons(SOSkinData currentData)
    {
        btnBuy.SetRequireValue(currentData.Price);
        btnBuy.SetActive(!currentData.IsUnlocked);
        btnSelect.SetActive(currentData.IsUnlocked && !currentData.IsChoosing);
    }
    private void InitNewCell()
    {
        SkinCell newCell = Instantiate(skinCells[0], cellHolder);
        skinCells.Add(newCell);
    }

    private SOSkinData[] GetAllSkins()
    {
        return SkinManager.Instance.AllSkinsOrdered;
    }

    private void OnClickBuy()
    {
        if (!cellSelected)
            return;

        var data = cellSelected.Info;
        sharedCoin.Value -= data.Price;
        SkinManager.Instance.UnlockSkin(data.IDReference);
        SkinManager.Instance.UpdateSkinSelected(data.IDReference);
        Reload();
        FocusCellSelected();
    }
    private void OnClickSelect()
    {
        if (!cellSelected)
            return;

        var data = cellSelected.Info;
        SkinManager.Instance.UpdateSkinSelected(data.IDReference);
        UpdateActiveButtons(data);
        Reload();
        FocusCellSelected();
    }
    private void OnClickHide()
    {
        this.Hide();
        MUIManager.Instance.ShowPanel<PanelHome>();
    }
    private void Callback_ClickSkinCell(SkinCell cell)
    {
        if (cellSelected)
            cellSelected.Unselect();

        cellSelected = cell;
        UpdatePriceText();
        UpdateActiveButtons(cell.Info);
    }
    public void UpdatePriceText()
    {
        if (!cellSelected)
        {
            return;
        }

        tmpPriceCurrent.text = $"{cellSelected.Info.Price}";
    }
}
