
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Switch : InteractTap
{
    [SerializeField, ReadOnly] private bool isOn;
    [SerializeField] private bool isStartTurnOn;
    [SerializeField] private SOAudio audioToggle;
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

    [Button]
    public void Toggle()
    {
        isOn = !isOn;
        Perform();

        if (audioToggle)
            audioToggle.Play();
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