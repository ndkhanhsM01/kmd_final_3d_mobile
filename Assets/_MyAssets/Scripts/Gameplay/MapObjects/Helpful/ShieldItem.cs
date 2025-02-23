

using UnityEngine;

public class ShieldItem : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOBoolEventChannel setEquipShieldChannel;
    public void Trigger(Transform source)
    {
        setEquipShieldChannel.Raise(true);
        gameObject.SetActive(false);
    }
}