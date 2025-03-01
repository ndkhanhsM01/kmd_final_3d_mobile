using MLib;
using UnityEngine;
using UnityEngine.UI;

public class PopupLevel: MPopup
{
    [SerializeField] private SOLevelsOrder levelOrder;
    [SerializeField] private Transform cellLevelsHolder;
    [SerializeField] private LevelCell levelCellPrefab;
    [SerializeField] private Button btnPlay;

    private bool isLoaded;

    protected override void OnEnable()
    {
        base.OnEnable();
        btnPlay.AddListener(OnClick_Play);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        btnPlay.RemoveListener(OnClick_Play);
    }

    public override void Show()
    {
        base.Show();

        if (!isLoaded)
            SetupLevels();

        isLoaded = true;
    }
    public void SetupLevels()
    {
        for (int i = 0; i < levelOrder.TotalLevels; i++)
        {
            LevelCell cell = Instantiate(levelCellPrefab, cellLevelsHolder);
            cell.Setup(i);
        }
    }

    private void OnClick_Play()
    {
        GameManager.Instance.EnterGame();
    }
}