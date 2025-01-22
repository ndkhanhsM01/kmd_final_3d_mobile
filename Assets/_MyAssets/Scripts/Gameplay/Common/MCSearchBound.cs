using UnityEngine;
using System;

public class MCSearchBound : MCSearchMachine
{
    [SerializeField] private float edge = 1f;
    [SerializeField] private Vector3 center = Vector3.zero;

    private Transform body;
    private Bounds bounds;
    private void Awake()
    {
        body = transform;
        bounds = new Bounds();
    }
    protected override bool CheckMCInside()
    {
        if (bounds == null) return false;

        bounds.center = body.position + center;
        bounds.extents = Vector3.one * (edge*0.5f + mc.Radius);
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