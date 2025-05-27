
using Sirenix.OdinInspector;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOMCState", menuName = "Gameplay/MainCharacter/SOMCState")]
public class SOMCState: ScriptableObject
{
    [ReadOnly, SerializeField] private MCState _current;

    private Action<MCState> onStateChanged;
    public MCState Current
    {
        get => _current;
        set
        {
            _current = value;
            onStateChanged?.Invoke(_current);
        }
    }

    public void AddListener(Action<MCState> callback)
    {
        onStateChanged += callback;
    }
    public void RemoveListener(Action<MCState> callback)
    {
        onStateChanged -= callback;
    }
}

[System.Serializable]
public enum MCState
{
    Alive,
    Dead 
}