using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOVector3EventChannel", menuName = "Events/SOVector3EventChannel")]
public class SOVector3EventChannel : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    private Action<Vector3> channel;
    public void Raise(Vector3 value)
    {
        channel?.Invoke(value);
    }

    public void Register(Action<Vector3> callback)
    {
        channel += callback;
    }
    public void Unregister(Action<Vector3> callback)
    {
        channel -= callback;
    }
}
