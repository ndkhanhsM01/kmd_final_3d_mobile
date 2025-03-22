
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MCDetector))]
public abstract class ObjectInteractable : MonoBehaviour
{
    [SerializeField] protected MCDetector detector;
    [Header("Events")]
    [SerializeField] protected SOVoidEventChannel tapChannel;
    [SerializeField] protected SOVoidEventChannel beginHoldChannel;
    [SerializeField] protected SOVoidEventChannel endHoldChannel;

    protected bool interactable;
    private Coroutine crHolding;
    protected MainCharacter mc => GameplayController.MC;
    protected virtual void Reset()
    {
        if(!detector)
            detector = gameObject.GetComponent<MCDetector>();
    }
    protected virtual void Start()
    {
        detector.StartScan();
    }
    protected virtual void OnEnable()
    {
        detector.Register_McEnter(OnMC_EnterZone);
        detector.Register_McExit(OnMC_ExitZone);
        detector.Register_McStay(OnMC_StayZone);
        tapChannel.Register(OnTap);
        beginHoldChannel.Register(OnBeginHold);
        endHoldChannel.Register(OnEndHold);
    }
    protected virtual void OnDisable()
    {
        detector.Unregister_McEnter(OnMC_EnterZone);
        detector.Unregister_McExit(OnMC_ExitZone);
        detector.Unregister_McStay(OnMC_StayZone);
        tapChannel.Unregister(OnTap);
        beginHoldChannel.Unregister(OnBeginHold);
        endHoldChannel.Unregister(OnEndHold);
    }
    protected virtual void OnMC_EnterZone()
    {
        interactable = true;
    }
    protected virtual void OnMC_ExitZone()
    {
        interactable = false;
        OnEndHold();
    }
    protected virtual void OnMC_StayZone()
    {

    }
    protected virtual void OnTap()
    {

    }
    protected virtual void OnBeginHold()
    {
        if (!interactable)
            return;

        crHolding = StartCoroutine(IE_PerformHolding());
    }
    protected virtual void OnEndHold()
    {
        if(crHolding != null)
            StopCoroutine(crHolding);
    }
    protected virtual void OnHolding()
    {

    }

    private IEnumerator IE_PerformHolding()
    {
        while (true)
        {
            OnHolding();
            yield return null;
        }
    }
}