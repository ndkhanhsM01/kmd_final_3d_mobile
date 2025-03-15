

using UnityEngine;

public class HarmfulArea: MonoBehaviour, ITriggerable
{
    [SerializeField] protected float force = 1f;
    [SerializeField] protected SOVector3EventChannel forceMcChannel;

    public virtual void Trigger(Transform source)
    {
        if (source.TryGetComponent(out MainCharacter mc) == false)
            return;

        if (mc.TryDeath())
        {
            Vector3 direction = (source.position - transform.position).normalized;
            mc.Action.ReceiveForce(direction * force);
        }
    }
}