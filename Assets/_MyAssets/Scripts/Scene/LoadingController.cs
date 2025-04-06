
using MLib;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MSingleton<LoadingController>
{
    [SerializeField] private SOFloatVariable sharedLoadingValue;
    [SerializeField] private Transform guiParent;
    [SerializeField] private Image imgFill;
    [SerializeField] private RectTransform transIcon;

    private float fillWidth = 960f;

    protected override void Awake()
    {
        base.Awake();
        RectTransform transFill = imgFill.transform as RectTransform;
        fillWidth = transFill.sizeDelta.x;
        sharedLoadingValue.Value = 0f;
    }
    private void OnEnable()
    {
        sharedLoadingValue.Register_OnValueChanged(OnLoading);
    }
    private void OnDisable()
    {
        sharedLoadingValue.Unregister_OnValueChanged(OnLoading);
    }

    public void Show(Action onComplete = null)
    {
        guiParent.SetActive(true);
        SetProgress(0);
    }
    public void Hide()
    {
        guiParent.SetActive(false);
    }
    public IEnumerator DoFillProgress(float from, float to, float duration)
    {
        float value = from;
        float speed = (to - from) / duration;
        while (value < to)
        {
            value += speed * Time.deltaTime;
            SetProgress(value);
            yield return null;
        }
    }
    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);
        sharedLoadingValue.Value = progress;
        UpdatePositionIcon(progress);
    }
    private void UpdatePositionIcon(float progress)
    {
        Vector2 newPosIcon = transIcon.anchoredPosition;
        newPosIcon.x = progress * fillWidth;
        transIcon.anchoredPosition = newPosIcon;
    }
    private void OnLoading(float value)
    {
        imgFill.fillAmount = value;
    }
#if UNITY_EDITOR
    [Button]
    private void ValidatePositionIcon()
    {
        if (transIcon)
        {
            RectTransform transFill = imgFill.transform as RectTransform;
            fillWidth = transFill.sizeDelta.x;
            UpdatePositionIcon(imgFill.fillAmount);
        }
        EditorUtility.SetDirty(this);
    }
#endif
}