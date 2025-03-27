
using DG.Tweening;
using UnityEngine;

public class ModelCharacterHomeScene: MonoBehaviour
{
    [SerializeField] private Vector3 eulerShopView;
    [SerializeField] private Transform body;
    [SerializeField] private CharacterSkin skin;

    [SerializeField] private SOVoidEventChannel showPanelChannel;
    [SerializeField] private SOVoidEventChannel hidePanelChannel;

    private Quaternion rotationDefault;
    private Quaternion rotationShop;

    private void Awake()
    {
        rotationDefault = body.rotation;
        rotationShop = Quaternion.Euler(eulerShopView);
    }

    private void OnEnable()
    {
        showPanelChannel.Register(OnShowPanel);
        hidePanelChannel.Register(OnHidePanel);
        PanelSkin.OnClickSkinCell += OnClickCellSkin;
    }
    private void OnDisable()
    {
        showPanelChannel.Unregister(OnShowPanel);
        hidePanelChannel.Unregister(OnHidePanel);
        PanelSkin.OnClickSkinCell -= OnClickCellSkin;
    }

    private void OnShowPanel()
    {

        Rotate(rotationShop);
    }
    private void OnHidePanel()
    {
        skin.PutOnSkinSelected();
        Rotate(rotationDefault);
    }

    private void Rotate(Quaternion rotation)
    {
        body.DORotateQuaternion(rotation, 0.5f);
    }

    private void OnClickCellSkin(SkinCell skinCell)
    {
        skin.PutOn(skinCell.Info.IDReference);
    }
}