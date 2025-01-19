

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialSelection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("SpecialSelection down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("SpecialSelection up");
    }
}