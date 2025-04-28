
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDoScaleOnOver: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Ease ease = Ease.Linear;
    [SerializeField] private float animDuration = 0.15f;
    [SerializeField] private float scaleDown = 0.9f;

    private RectTransform rectTransform;
    private Vector3 defaultScale;
    private void Awake()
    {
        rectTransform = transform as RectTransform;
        defaultScale = rectTransform.localScale;
    }
    private void OnDisable()
    {
        DOTween.Kill(rectTransform);
    }
    private void OnDestroy()
    {
        DOTween.Kill(rectTransform);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Kill(rectTransform);
        rectTransform.DOScale(scaleDown * defaultScale, animDuration)
            .SetEase(ease)
            .SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DOTween.Kill(rectTransform);
        rectTransform.DOScale(defaultScale, animDuration)
            .SetEase(ease)
            .SetUpdate(true);
    }
}