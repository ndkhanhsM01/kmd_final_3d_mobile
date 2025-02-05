using MLib;
using System.Collections;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float interval = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PoolHostileProjectile arrowSpawner;

    private void Awake()
    {
        arrowSpawner.Initialize(transform);
    }

    private void Start()
    {
        StartCoroutine(IE_FireLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator IE_FireLoop()
    {
        yield return new WaitForSeconds(delay);

        var waiter = new WaitForSeconds(interval);
        while (true)
        {
            Fire();
            yield return waiter;
        }
    }

    [MButton]
    public void Fire()
    {
        HostileProjectile arrow = arrowSpawner.GetItem();
        arrow.transform.parent = null;
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
