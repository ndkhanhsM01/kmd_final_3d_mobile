
using MLib;
using UnityEngine;
using UnityEngine.Events;

public class Switch : InteractTap
{
    [SerializeField, ReadOnly] private bool isOn;
    [SerializeField] private bool isStartTurnOn;
    [SerializeField] private GameObject goStatusOn;
    [SerializeField] private GameObject goStatusOff;

    [Header("Events")]
    [SerializeField] private UnityEvent onSwitchOn;
    [SerializeField] private UnityEvent onSwitchOff;


    protected override void Start()
    {
        base.Start();
        isOn = isStartTurnOn;
        Perform();
    }

    public void Toggle()
    {
        isOn = !isOn;
        Perform();
    }
    private void Perform()
    {
        if (isOn)
            onSwitchOn?.Invoke();
        else
            onSwitchOff?.Invoke();

        goStatusOn.SetActive(isOn);
        goStatusOff.SetActive(!isOn);
    }

    protected override void OnTap()
    {
        Toggle();
    }

    protected override bool CheckInteractable()
    {
        return true;
    }
}