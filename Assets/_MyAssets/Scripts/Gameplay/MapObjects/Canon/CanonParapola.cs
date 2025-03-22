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

    [Header("Configure")]
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private float preCaculateTime = 1f;
    [SerializeField] private AnimationCurve yCurve;

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

        StartCoroutine(IE_DirectBullet());
        StartCoroutine(IE_RestoreCooldown());
    }

    private IEnumerator IE_RestoreCooldown()
    {
        yield return waitForCooldown;

        detector.StartScan();
    }

    private IEnumerator IE_DirectBullet()
    {
        CanonBullet bullet = pool.GetItem();
        Transform bulletTrans = bullet.transform;
        bullet.SetActive(true);
        bulletTrans.position = transform.position;

        Vector3 targetPoint = CaculateTargetPoint();
        Vector3 direction = (targetPoint - transform.position).normalized;
        float length = Vector3.Distance(transform.position, targetPoint);
        float totalTime = length / speed;
        float timer = 0f;
        float step = 0f;
        Vector3 newPosition = bulletTrans.position;
        while (ReachedTarget() == false)
        {
            timer += Time.deltaTime;
            float lerpValue = yCurve.Evaluate(timer / totalTime);
            step = speed * Time.deltaTime;
            newPosition += step * direction;
            newPosition.y = transform.position.y + maxHeight * lerpValue;
            bulletTrans.position = newPosition;

            yield return null;
        }

        bullet.Explode();

        bool ReachedTarget()
        {
            return Vector3.Distance(bulletTrans.position, targetPoint) < speed * Time.deltaTime;
            //return timer < totalTime;
        }
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

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying == false)
            return;

        Vector3 point = CaculateTargetPoint();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(point, .3f);
    }
}
