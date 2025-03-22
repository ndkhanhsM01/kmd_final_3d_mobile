using DG.Tweening;
using MLib;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MCDetector))]
public class CanonParapola : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PoolCanonBullet pool;
    [SerializeField] private MCDetector detector;
    [SerializeField] private SOVector3Variable mcForward;
    [SerializeField] private SOMcDefaultStats mcStats;
    [SerializeField] private ParticleSystem fxShoot;

    [Header("Body")]
    [SerializeField] private float turnDuration = 3f;
    [SerializeField] private Transform pivotCanon;
    [SerializeField] private Transform shootPoint;

    [Header("Configure")]
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private float preCaculateTime = 1f;

    private WaitForSeconds waitForCooldown;
    private void Awake()
    {
        waitForCooldown = new WaitForSeconds(cooldown);
    }

    private void OnEnable()
    {
        detector.StartScan();
        detector.Register_McEnter(OnMCEnter);
    }
    private void OnDisable()
    {
        detector.Unregister_McEnter(OnMCEnter);
        detector.StopScan();

        StopAllCoroutines();
    }

    [Button]
    public void Shoot()
    {
        detector.StopScan();

        StartCoroutine(IE_ShootBullet());
    }

    private IEnumerator IE_RestoreCooldown()
    {
        yield return waitForCooldown;

        detector.StartScan();
    }

    private IEnumerator IE_ShootBullet()
    {
        Vector3 targetPoint = CaculateTargetPoint();
        yield return StartCoroutine(IE_LookTowardTarget(targetPoint));

        DirectBullet(targetPoint);

        StartCoroutine(IE_RestoreCooldown());
    }

    private IEnumerator IE_LookTowardTarget(Vector3 targetPoint)
    {
        Vector3 toward = (targetPoint - pivotCanon.position).normalized;
        float angle = Mathf.Atan2(toward.z, toward.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -angle + 180f);
        float elapsedTime = 0f;
        while (elapsedTime < turnDuration)
        {
            elapsedTime += Time.deltaTime;
            pivotCanon.localRotation = Quaternion.Lerp(pivotCanon.localRotation, targetRotation, elapsedTime / turnDuration);
            yield return null;
        }

        pivotCanon.localRotation = targetRotation;
    }

    private void DirectBullet(Vector3 targetPoint)
    {
        fxShoot.Play();
        CanonBullet bullet = pool.GetItem();
        Transform bulletTrans = bullet.transform;
        bullet.SetActive(true);
        bulletTrans.position = transform.position;

        bullet.Move(shootPoint.position, targetPoint, maxHeight);
    }
    private Vector3 CaculateTargetPoint()
    {
        float lengthPreCaculate = mcStats.MoveSpeed * preCaculateTime * Time.deltaTime;
        Transform mcBody = GameplayController.MC.Body;
        Vector3 targetPoint = mcBody.position + mcForward.Value.normalized * lengthPreCaculate;
        targetPoint.y = transform.position.y;

        return targetPoint;
    }

    private void OnMCEnter()
    {
        Shoot();
    }
}
