
using MLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIToggleStatus : MonoBehaviour
{
    [SerializeField] private SOBoolEventChannel boolChannel;
    [SerializeField] private GameObject on, off;

    public void UpdateStatus(bool isOn)
    {
        on.SetActive(isOn);
        off.SetActive(!isOn);

        if (boolChannel)
            boolChannel.Raise(isOn);
    }
}