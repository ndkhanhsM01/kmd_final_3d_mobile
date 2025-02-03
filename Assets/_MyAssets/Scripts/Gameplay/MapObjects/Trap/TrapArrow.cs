using MLib;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private PoolHostileProjectile arrowSpawner;

    private void Awake()
    {
        arrowSpawner.Initialize(firePoint);
    }

    [MButton]
    public void Fire()
    {
        HostileProjectile arrow = arrowSpawner.GetItem();
        arrow.SetActive(true);
        arrow.transform.position = firePoint.position;
        arrow.Move(firePoint.forward);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(firePoint.position, 0.2f);
    }
#endif
}
