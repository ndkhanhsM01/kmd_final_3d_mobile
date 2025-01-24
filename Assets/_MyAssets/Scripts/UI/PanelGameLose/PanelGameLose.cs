using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelGameLose : MPanel
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnReplay;

    private void OnEnable()
    {
        btnHome.AddListener(OnClick_Home);
        btnReplay.AddListener(OnClick_Replay);
    }
    private void OnDisable()
    {
        btnHome.RemoveListener(OnClick_Home);
        btnReplay.RemoveListener(OnClick_Replay);
    }

    private void OnClick_Home()
    {
        LoadSceneManager.Instance.Load_Home();
    }
    private void OnClick_Replay()
    {
        LoadSceneManager.Instance.ReloadCurScene();
    }
}
