

using MLib;
using UnityEngine;
using UnityEngine.UI;

public class PanelConfirmReplay: MPanel
{
    [SerializeField] private Button btnAccept;
    [SerializeField] private Button btnRefuse;

    private void OnEnable()
    {
        btnAccept.AddListener(OnClick_Accept);
        btnRefuse.AddListener(OnClick_Refuse);
    }
    private void OnDisable()
    {
        btnAccept.RemoveListener(OnClick_Accept);
        btnRefuse.RemoveListener(OnClick_Refuse);
    }

    private void OnClick_Accept()
    {
    }
    private void OnClick_Refuse()
    {

    }
}