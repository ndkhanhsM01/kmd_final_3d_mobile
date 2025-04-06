
using MLib;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOFloatVariable", menuName = "SharedVariables/SOFloatVariable")]
public class SOFloatVariable : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    [SerializeField, ReadOnly] private float _value;
    private Action<float> onValueChanged;
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            onValueChanged?.Invoke(_value);
        }
    }

    public void Register_OnValueChanged(Action<float> callback)
    {
        onValueChanged += callback;
    }
    public void Unregister_OnValueChanged(Action<float> callback)
    {
        onValueChanged -= callback;
    }
}

