
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIKeySlot: MonoBehaviour
{
    [SerializeField] private Outline outline;

    public void Setup(Color color)
    {
        color.a = outline.effectColor.a;
        outline.effectColor = color;
    }
    public void Show()
    {
        Transform body = transform;
        body.DOKill();
        gameObject.SetActive(true);

        body.localScale = Vector3.zero;
        var sequence = DOTween.Sequence();
        sequence.Append(body.DOScale(1.15f, 0.2f))
                .Append(body.DOScale(1f, 0.07f));
    }
    public void Hide(Action onHideComplete)
    {
        RectTransform body = transform as RectTransform;
        body.DOKill();
        var sequence = DOTween.Sequence();
        sequence.Append(body.DOScale(1.15f, 0.07f))
                .Append(body.DOScale(0f, 0.2f))
                .Append(DOVirtual.DelayedCall(0.15f, null));

        sequence.OnComplete(() =>
        {
            onHideComplete.Invoke();
            gameObject.SetActive(false);
        });
    }
}