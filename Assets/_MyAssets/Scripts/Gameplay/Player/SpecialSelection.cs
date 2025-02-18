

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecialSelection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image image;
    [Header("Configure")]
    [SerializeField, Min(0f)] private float minTimeToHold = 0.15f;

    public Action OnTap;
    public Action OnBeginHolding;
    public Action OnEndHolding;
    public Action OnReleased;

    private bool isPressed;
    private bool isHolding;

    public bool IsHolding => isHolding;

    private Coroutine crCheckHolding;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        image.color = Color.green;
        StartCheckHolding();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isHolding)
            OnEndHolding?.Invoke();

        isHolding = false;
        isPressed = false;
        image.color = Color.white;

        OnReleased?.Invoke();
    }

    private void StartCheckHolding()
    {
        StopCheckHolding();
        crCheckHolding = StartCoroutine(IE_CheckHolding());
    }

    private void StopCheckHolding()
    {
        if(crCheckHolding != null)
            StopCoroutine(crCheckHolding);
    }

    private IEnumerator IE_CheckHolding()
    {
        if(!isPressed)
            yield break;

        float timer = 0f;
        isHolding = false;

        while(timer <= minTimeToHold)
        {
            if(!isPressed)
            {
                OnTap?.Invoke();
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if(isPressed)
        {
            OnBeginHolding?.Invoke();
            isHolding = true;
        }
    }
}