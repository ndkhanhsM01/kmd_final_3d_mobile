
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationScalePopup: MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject cover;

    [Header("Show")]
    [SerializeField] private float durationShow = 0.25f;
    [SerializeField] private float delayShow = 0f;
    [SerializeField] private Ease easeShow = Ease.OutBack;
    [SerializeField] private Vector3 showValue = Vector3.one;

    [Header("Hide")]
    [SerializeField] private float durationHide = 0.25f;
    [SerializeField] private float delayHide = 0f;
    [SerializeField] private Ease easeHide = Ease.InBack;
    [SerializeField] private Vector3 hideValue = Vector3.zero;

    private Tween tween;

    [Button]
    public void DoShow()
    {
        if (tween != null) tween.Kill();

        target.localScale = hideValue;
        tween = target.DOScale(showValue, durationShow).SetEase(easeShow);
        tween.SetDelay(delayShow);
        if (cover)
            cover.SetActive(true);
    }

    [Button]
    public void DoHide()
    {
        if (tween != null) tween.Kill();

        tween = target.DOScale(hideValue, durationHide).SetEase(easeHide);
        tween.SetDelay(delayHide);
        tween.SetUpdate(true);
        tween.OnComplete(() =>
        {
            if (cover)
                cover.SetActive(false);
        });
    }

    private void OnDestroy()
    {
        if (tween != null) tween.Kill();

    }
}