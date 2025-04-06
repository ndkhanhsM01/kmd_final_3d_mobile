
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOBoolVariable", menuName = "SharedVariables/SOBoolVariable")]
public class SOBoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    [SerializeField] private bool _value;
    private Action<bool> onValueChanged;
    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            onValueChanged?.Invoke(_value);
        }
    }

    public void Register_OnValueChanged(Action<bool> callback)
    {
        onValueChanged += callback;
    }
    public void Unregister_OnValueChanged(Action<bool> callback)
    {
        onValueChanged -= callback;
    }
}
