using MLib;
using System.Collections;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float interval = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PoolHostileProjectile arrowSpawner;

    private bool isFiring;

    private void Awake()
    {
        arrowSpawner.Initialize(transform);
    }

    private void OnEnable()
    {
        if(isFiring)
            StartCoroutine(IE_FireLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        StartCoroutine(IE_FireLoop());
    }

    private IEnumerator IE_FireLoop()
    {
        isFiring = true;
        yield return new WaitForSeconds(delay);

        var waiter = new WaitForSeconds(interval);
        while (isFiring)
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
