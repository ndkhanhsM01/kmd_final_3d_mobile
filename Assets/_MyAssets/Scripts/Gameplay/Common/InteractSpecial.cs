
using UnityEngine;

public class InteractSpecial: MonoBehaviour
{
    [System.Serializable]
    public class Info
    {
        public SpecialSelection.InputType type;
        public string Description;
    }

    [Header("Base")]
    [SerializeField] protected SOVoidEventChannel tapChannel;
    [SerializeField] protected SOVoidEventChannel beginHoldChannel;
    [SerializeField] protected SOVoidEventChannel endHoldChannel;
    [SerializeField] protected Info info;


    protected virtual void OnEnable()
    {
        tapChannel.Register(OnTap);
        beginHoldChannel.Register(OnBeginHold);
        endHoldChannel.Register(OnEndHold);
    }
    protected virtual void OnDisable()
    {
        tapChannel.Unregister(OnTap);
        beginHoldChannel.Unregister(OnBeginHold);
        endHoldChannel.Unregister(OnEndHold);
    }

    protected virtual void ShowInteractGUI()
    {
        SetActiveInteractGUI(true);
    }
    protected virtual void HideInteractGUI()
    {
        SetActiveInteractGUI(false);
    }

    protected virtual void SetActiveInteractGUI(bool active)
    {
        if (CheckInteractable() == false)
            active = false;

        if (active)
            SpecialSelection.RequestShow(info);
        else
            SpecialSelection.RequestShow(null);
    }

    protected virtual void OnTap()
    {

    }
    protected virtual void OnBeginHold()
    {

    }
    protected virtual void OnEndHold()
    {
    }
    protected virtual bool CheckInteractable()
    {
        return true;
    }
}