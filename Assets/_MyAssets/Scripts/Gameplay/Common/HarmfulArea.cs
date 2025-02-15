

using UnityEngine;

public class HarmfulArea: MonoBehaviour, ITriggerable
{
    [SerializeField] private float force = 1f;
    [SerializeField] private SOVector3EventChannel forceMcChannel;

    public void Trigger(Transform interaction)
    {
        Vector3 direction = (interaction.position - transform.position).normalized;
        forceMcChannel.Raise(direction * force);
        GameplayController.Instance.LoseLevelDelay(0.75f);
    }
}