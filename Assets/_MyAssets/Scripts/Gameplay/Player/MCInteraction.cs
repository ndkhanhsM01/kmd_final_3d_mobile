
using System;
using UnityEngine;

public class MCInteraction: MonoBehaviour
{
    public Action OnCollidedHarmful;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(CustomTags.Triggerable))
        {
            Debug.Log(collision.gameObject.name);
            Perform(collision.gameObject);
        }
    }

    private void Perform(GameObject target)
    {
        if (!target.TryGetComponent(out ITriggerable harmful)) 
            return;

        OnCollidedHarmful?.Invoke();
        harmful.Trigger();
    }
}