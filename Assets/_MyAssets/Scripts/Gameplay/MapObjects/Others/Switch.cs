
using MLib;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, ITriggerable
{
    [SerializeField, ReadOnly] private bool isOn;
    [SerializeField] private bool isStartTurnOn;
    [SerializeField] private GameObject goStatusOn;
    [SerializeField] private GameObject goStatusOff;

    [Header("Events")]
    [SerializeField] private UnityEvent onSwitchOn;
    [SerializeField] private UnityEvent onSwitchOff;


    private void Start()
    {
        isOn = isStartTurnOn;
        Perform();
    }

    public void Trigger(Transform interaction)
    {
        Toggle();
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
}