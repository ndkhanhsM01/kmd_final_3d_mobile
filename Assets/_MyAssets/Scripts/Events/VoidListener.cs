

using System;
using UnityEngine;
using UnityEngine.Events;

public class VoidListener : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField, TextArea] string description;
#endif
    [SerializeField] private SOVoidEventChannel channel;
    public UnityEvent callback;

    private void OnEnable()
    {
        channel.Register(callback.Invoke);
    }
    private void OnDisable()
    {
        channel.Unregister(callback.Invoke);
    }
}