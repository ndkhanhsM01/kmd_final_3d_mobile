using MLib;
using System.Collections;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private float delay;
    [SerializeField] private float interval = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem fxFire;
    [SerializeField] private PoolHostileProjectile arrowSpawner;

    private bool isFiring;

    private void OnEnable()
    {
        if (isFiring)
            FireLoop();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        if (playOnStart)
            FireLoop();
    }

    public void FireLoop()
    {
        StopFire();
        StartCoroutine(IE_FireLoop());

    }
    public void StopFire()
    {
        StopAllCoroutines();
        isFiring = false;
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
        fxFire.Play();
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
