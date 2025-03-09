
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MLib;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;

public class ShieldEquipment: MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private SOBoolVariable godStatus;
    [SerializeField] private SOBoolEventChannel setEquipChannel;
    [SerializeField] private SpriteRenderer sprFill;

    private bool shieldEnabled;
    private int paramFill = Shader.PropertyToID("_Arc1");

    private Vector3 defaultScaleVisual;
    private void Awake()
    {
        defaultScaleVisual = visual.localScale;
        visual.SetActive(false);
        sprFill.SetActive(false);
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
        godStatus.Value = true;
        shieldEnabled = true;
        DoShowVisual();

        sprFill.SetActive(true);
        sprFill.sharedMaterial.SetFloat(paramFill, 0f);
    }
    private async void DestroyShield()
    {
        if (!shieldEnabled)
            return;

        shieldEnabled = false;
        DoHideVisual();

        float delay = 3f;
        float timer = 0f;
        while(timer < delay)
        {
            timer += Time.deltaTime;
            sprFill.sharedMaterial.SetFloat(paramFill, (timer / delay) * 360f);
            await UniTask.WaitForEndOfFrame();
        }

        sprFill.SetActive(false);
        godStatus.Value = false;
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