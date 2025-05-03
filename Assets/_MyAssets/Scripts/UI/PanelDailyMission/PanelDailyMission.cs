using UnityEngine;
using MLib;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PanelDailyMission : MPanel
{
    [SerializeField] private SODailyMissionOrdered dataOredered;
    [SerializeField] private UIDailyMissionCell cellPrefab;
    [SerializeField] private RectTransform cellHolder;
    [SerializeField] private Button btnClose;
    [SerializeField] private List<UIDailyMissionCell> cellList;

    private bool init = false;

    private void OnEnable()
    {
        btnClose.AddListener(OnClick_Close);
    }
    private void OnDisable()
    {
        btnClose.RemoveListener(OnClick_Close);
    }
    public override void Show(Action onFinish)
    {
        base.Show(onFinish);
        if (!init)
            Setup();
        else
            Reload();
    }

    private void Setup()
    {
        cellList = new();
        foreach (var mission in dataOredered.Missions)
        {
            var newCell = Instantiate(cellPrefab, cellHolder);
            newCell.SetActive(true);
            newCell.Setup(mission);
            cellList.Add(newCell);
        }

        init = true;
    }
    private void Reload()
    {
        foreach(var cell in cellList)
            cell.Reload();
    }
    private void SortMissions()
    {

    }
    private void OnClick_Close()
    {
        Hide();
    }
}
