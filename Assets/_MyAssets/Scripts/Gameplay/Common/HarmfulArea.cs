

using UnityEngine;

public class HarmfulArea: MonoBehaviour, ITriggerable
{

    public void Trigger()
    {
        GameplayController.Instance.LoseLevel();
    }
}