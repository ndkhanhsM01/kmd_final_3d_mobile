

using UnityEngine;

public class HarmfulArea: MonoBehaviour
{
    [SerializeField] protected float force = 1f;

    private Transform body;
    private void Awake()
    {
        body = transform;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        Transform otherTrans = other.transform;
        if (otherTrans.TryGetComponent(out IReceiveDamage receiver) == false)
            return;

        Vector3 direction = (otherTrans.position - body.position).normalized;
        if (receiver.ReceiveDamage(body))
        {
            receiver.ReceiveForce(direction * force);
        }
    }/*
    public virtual void Trigger(Transform source)
    {

    }*/
}