
using MLib;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOIntVariable", menuName = "SharedVariables/SOIntVariable")]
public class SOIntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    [SerializeField] private int _value;
    private Action<int> onValueChanged;
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            onValueChanged?.Invoke(_value);
        }
    }

    public void Register_OnValueChanged(Action<int> callback)
    {
        onValueChanged += callback;
    }
    public void Unregister_OnValueChanged(Action<int> callback)
    {
        onValueChanged -= callback;
    }
}

