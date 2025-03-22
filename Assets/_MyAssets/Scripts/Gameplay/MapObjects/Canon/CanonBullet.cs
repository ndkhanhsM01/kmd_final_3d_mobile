
using System;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{
    [SerializeField] private float affectZone = 2f;
    [SerializeField] private float forceUp = 30f;
    [SerializeField] protected SOVector3EventChannel forceMcChannel;

    private Vector3 extents;
    private Action onExpodeEnd;
    private void Awake()
    {
        extents = Vector3.one * affectZone;
    }

    public void Explode()
    {
        onExpodeEnd?.Invoke();
        Collider[] colliders = Physics.OverlapBox(transform.position, extents);
        if (colliders == null || colliders.Length <= 0)
            return;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IReceiveDamage receiver))
            {
                receiver.ReceiveDamage(transform);
                receiver.ReceiveForce(Vector3.up * forceUp);
            }
        }
    }

    public void SetOnExpodeEnd(Action callback)
    {
        onExpodeEnd = callback;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, Vector3.one * affectZone);
    }
}