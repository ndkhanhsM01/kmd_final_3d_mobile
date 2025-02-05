
using UnityEngine;

public class Prison : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOPrisonKeyReference keyStorage;
    [SerializeField] private Hostage hostage;

    public void Trigger()
    {
        if (keyStorage.TryUnlockPrison(this))
        {
            Unlock();
            gameObject.SetActive(false);
        }
        else
        {
        }
    }
    private void Unlock()
    {
        hostage.transform.parent = transform.parent;
        hostage.Releaseable();
    }
}