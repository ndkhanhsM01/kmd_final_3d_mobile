
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MLib;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;

public class ShieldEquipment: MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private GodStatusHandler godStatus;
    [SerializeField] private SOBoolEventChannel setEquipChannel;

    private bool shieldEnabled;

    private Vector3 defaultScaleVisual;
    private void Awake()
    {
        defaultScaleVisual = visual.localScale;
        visual.SetActive(false);
    }

    private void OnEnable()
    {
        setEquipChannel.Register(OnSetEquip);
    }
    private void OnDisable()
    {
        setEquipChannel.Unregister(OnSetEquip);
    }

    private void OnSetEquip(bool value)
    {
        if (value)
        {
            EquipShield();
        }
        else
        {
            DestroyShield();
        }
    }

    private void EquipShield()
    {
        godStatus.Active();
        shieldEnabled = true;
        DoShowVisual();

    }
    private void DestroyShield()
    {
        if (!shieldEnabled)
            return;

        shieldEnabled = false;
        DoHideVisual();
        godStatus.Deactive();
    }

    [Button]
    private void DoShowVisual()
    {
        visual.SetActive(true);
        visual.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(visual.DOScale(defaultScaleVisual * 1.1f, 0.25f))
                .Append(visual.DOScale(defaultScaleVisual, 0.1f));
    }

    [Button]
    private void DoHideVisual()
    {
        visual.DOKill();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(visual.DOScale(defaultScaleVisual * 1.1f, 0.07f))
                .Append(visual.DOScale(0f, 0.15f));
    }
}