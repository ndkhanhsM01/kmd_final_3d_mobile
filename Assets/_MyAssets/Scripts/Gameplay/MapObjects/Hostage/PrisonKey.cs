
using MLib;
using UnityEngine;

public class PrisonKey : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOPrisonKeyReference storage;
    public void Trigger()
    {
        storage.AddKey(this);
        this.SetActive(false);
    }
}