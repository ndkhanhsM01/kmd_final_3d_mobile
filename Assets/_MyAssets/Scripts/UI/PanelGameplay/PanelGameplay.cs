
using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelGameplay: MPanel
{
    [SerializeField] private Button btnBackHome;

    private void OnEnable()
    {
        btnBackHome.AddListener(OnClick_BackHome);
    }
    private void OnDisable()
    {
        btnBackHome.RemoveListener(OnClick_BackHome);
    }

    private void OnClick_BackHome()
    {
        LoadSceneManager.Instance.Load_Home();
    }
}