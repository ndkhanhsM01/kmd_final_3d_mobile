using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PopupConfirmReplay: MPopup
{
    [SerializeField] private Button btnAccept;
    [SerializeField] private Button btnRefuse;

    protected override void OnEnable()
    {
        base.OnEnable();
        btnAccept.AddListener(OnClick_Accept);
        btnRefuse.AddListener(OnClick_Refuse);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        btnAccept.RemoveListener(OnClick_Accept);
        btnRefuse.RemoveListener(OnClick_Refuse);
    }

    private void OnClick_Accept()
    {
        DataManager.Instance.RenewLevel();
        GameManager.Instance.EnterGame();
    }
    private void OnClick_Refuse()
    {
        Hide();
    }
}