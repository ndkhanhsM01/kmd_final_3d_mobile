
using UnityEngine;

[RequireComponent(typeof(MCDetector))]
public abstract class InteractSpecial: MonoBehaviour
{
    [System.Serializable]
    public class Info
    {
        public SpecialSelection.InputType type;
        public string Description;
    }

    [SerializeField] protected Info info;
    [SerializeField] protected bool scanOnStart = true;
    [SerializeField] protected MCDetector mcDetector;

    protected virtual void Start()
    {
        if (scanOnStart)
            mcDetector.StartScan();
    }

    protected virtual void Reset()
    {
        mcDetector = GetComponent<MCDetector>();
    }
    protected virtual void OnEnable()
    {
        mcDetector.Register_McEnter(ShowInteractGUI);
        mcDetector.Register_McExit(HideInteractGUI);
    }
    protected virtual void OnDisable()
    {
        mcDetector.Unregister_McEnter(ShowInteractGUI);
        mcDetector.Unregister_McExit(HideInteractGUI);
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
        if (active && CheckInteractable() == false)
            active = false;

        if (active)
            SpecialSelection.RequestShow(info, OnTap, OnBeginHold, OnEndHold);
        else
            SpecialSelection.RequestShow(null);
    }
    protected abstract bool CheckInteractable();
    protected virtual void OnTap() { }
    protected virtual void OnBeginHold() { }
    protected virtual void OnEndHold() { }
}