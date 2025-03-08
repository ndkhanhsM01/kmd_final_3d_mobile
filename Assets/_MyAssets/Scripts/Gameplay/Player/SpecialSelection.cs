

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using InfoInteract = InteractSpecial.Info;

public class SpecialSelection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum InputType
    {
        Tap,
        Hold
    }

    [SerializeField] private Image imgHandle;
    [SerializeField] private TMP_Text tmpNameInteract;
    [SerializeField] private TMP_Text tmpDesInteract;
    [SerializeField] private string txtTap = "Tap";
    [SerializeField] private string txtHold = "Hold";
    [SerializeField] private GameObject parentGUI;
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

    private static Action<InfoInteract> evtRequestShow;

    private void OnEnable()
    {
        evtRequestShow += OnRequestShow;
    }
    private void OnDisable()
    {
        evtRequestShow -= OnRequestShow;
    }

    private void Start()
    {
        parentGUI.SetActive(false);
    }

    private void OnRequestShow(InfoInteract info)
    {
        if(info != null)
        {
            parentGUI.SetActive(true);
            tmpNameInteract.text = info.type == InputType.Tap ? txtTap : txtHold;
            tmpDesInteract.text = info.Description;
        }
        else
        {
            parentGUI.SetActive(false);
        }
    }

    public static void RequestShow(InfoInteract info)
    {
        evtRequestShow?.Invoke(info);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        imgHandle.color = Color.green;
        StartCheckHolding();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isHolding)
            OnEndHolding?.Invoke();

        isHolding = false;
        isPressed = false;
        imgHandle.color = Color.white;

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