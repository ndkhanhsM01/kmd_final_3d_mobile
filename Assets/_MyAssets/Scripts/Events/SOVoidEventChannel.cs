using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOVoidEventChannel", menuName = "Events/SOVoidEventChannel")]
public class SOVoidEventChannel: ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    private Action channel;
    public void Raise()
    {
        channel?.Invoke();
    }

    public void Register(Action callback)
    {
        channel += callback;
    }
    public void Unregister(Action callback)
    {
        channel -= callback;
    }
}