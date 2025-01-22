using UnityEngine;
using System;

public class MCSearchBound : MCSearchMachine
{
    [SerializeField] private float edge = 1f;
    [SerializeField] private Vector3 center = Vector3.zero;

    private Bounds bounds;
    private void Awake()
    {
        bounds = new Bounds(transform.position + center, Vector3.one * edge);
    }
    protected override bool CheckMCInside()
    {
        if (bounds == null) return false;
        return bounds.Contains(mc.Body.position);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + center, Vector3.one * edge);
    }
#endif
}