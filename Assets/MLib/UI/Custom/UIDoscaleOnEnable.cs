

using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UIDoscaleOnEnable : MonoBehaviour
{
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private float delay = 0f;
    [SerializeField] private Ease ease = Ease.OutBack;
    [SerializeField] private Vector3 startValue = Vector3.zero;
    [SerializeField] private Vector3 endValue = Vector3.one;

    [Header("Events")]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onComplete;

    private Tween tween;

    private void OnEnable()
    {
        DoScale();
    }

    private void OnDisable()
    {
        if (tween != null) tween.Kill();
    }

    private void DoScale()
    {
        if (tween != null) tween.Kill();
        Transform target = transform;

        target.localScale = startValue;
        tween = target.DOScale(endValue, duration).SetEase(ease);
        tween.SetDelay(delay);

        onStart?.Invoke();
        tween.OnComplete(() => { onComplete?.Invoke(); });
    }
}