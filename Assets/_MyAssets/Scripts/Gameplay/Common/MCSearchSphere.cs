using UnityEngine;
public class MCSearchSphere : MCSearchMachine
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Vector3 center;

    private Transform body;
    private void Awake()
    {
        body = transform;
    }
    protected override bool CheckMCInside()
    {
        if(!body) return false;
        return Vector3.Distance(body.position + center, mc.Body.position) < radius + mc.Radius;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + center, radius);
    }
#endif
}