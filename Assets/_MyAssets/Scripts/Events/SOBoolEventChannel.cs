using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOBoolEventChannel", menuName = "Events/SOBoolEventChannel")]
public class SOBoolEventChannel : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    private Action<bool> channel;
    public void Raise(bool value)
    {
        channel?.Invoke(value);
    }

    public void Register(Action<bool> callback)
    {
        channel += callback;
    }
    public void Unregister(Action<bool> callback)
    {
        channel -= callback;
    }
}