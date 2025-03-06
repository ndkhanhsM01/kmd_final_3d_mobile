using UnityEngine;
using UnityEngine.UI;
using MLib;

public class PanelGameWin: MPanel
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnNext;

    private void OnEnable()
    {
        btnHome.AddListener(OnClick_Home);
        btnNext.AddListener(OnClick_Next);
    }
    private void OnDisable()
    {
        btnHome.RemoveListener(OnClick_Home);
        btnNext.RemoveListener(OnClick_Next);
    }

    private void OnClick_Home()
    {
        LoadSceneManager.Instance.Load_Home();
    }
    private void OnClick_Next()
    {
        //LoadSceneManager.Instance.Load_Gameplay();
        GameManager.Instance.EnterGame();
    }
}