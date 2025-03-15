
using UnityEngine;
using UnityEngine.Events;

public class AreaTriggerEvent : MonoBehaviour, ITriggerable
{
    [SerializeField] private UnityEvent trigger;
    public void Trigger(Transform source)
    {
        trigger?.Invoke();
    }
}