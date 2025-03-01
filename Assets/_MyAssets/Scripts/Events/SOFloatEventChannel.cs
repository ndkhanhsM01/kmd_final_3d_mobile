using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOFloatEventChannel", menuName = "Events/SOFloatEventChannel")]
public class SOFloatEventChannel : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    private Action<float> channel;
    public void Raise(float value)
    {
        channel?.Invoke(value);
    }

    public void Register(Action<float> callback)
    {
        channel += callback;
    }
    public void Unregister(Action<float> callback)
    {
        channel -= callback;
    }
}