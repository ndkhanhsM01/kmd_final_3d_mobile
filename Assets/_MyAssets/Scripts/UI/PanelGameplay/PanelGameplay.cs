
using UnityEngine;
using MLib;
using UnityEngine.UI;
using TMPro;

public class PanelGameplay: MPanel
{
    [SerializeField] private TMP_Text tmpHostage;
    [SerializeField] private Button btnBackHome;

    private void OnEnable()
    {
        btnBackHome.AddListener(OnClick_BackHome);
    }
    private void OnDisable()
    {
        btnBackHome.RemoveListener(OnClick_BackHome);
    }
    public void SetHostageFreedom(int amountFreedom, int total)
    {
        tmpHostage.text = $"{amountFreedom}/{total}";
    }

    private void OnClick_BackHome()
    {
        LoadSceneManager.Instance.Load_Home();
    }
}