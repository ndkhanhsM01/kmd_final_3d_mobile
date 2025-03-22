
using Cysharp.Threading.Tasks;
using MLib;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CanonBullet : MonoBehaviour
{
    [SerializeField] protected SOVector3EventChannel forceMcChannel;
    [SerializeField] private float delayReturn = 2f;

    [Header("Movement")]
    [SerializeField] private Transform body;
    [SerializeField] private float affectZone = 2f;
    [SerializeField] private float forceAffectMC = 30f;
    [SerializeField] private float forceShootMultiplier = 30f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private AnimationCurve yCurve;

    [Header("Warning")]
    [SerializeField] private Gradient colorWarning;
    [SerializeField] private SpriteRenderer renderWarning;
    [SerializeField] private ParticleSystem fxExplosion;

    [Header("Events")]
    [SerializeField] private UnityEvent onExpoded;

    private Vector3 extents;
    private Action onReturnPool;
    private Tween tMove;
    private void Awake()
    {
        extents = Vector3.one * affectZone;
    }
    private void OnDisable()
    {
        if (tMove != null)
            tMove.Kill();
    }

    public void Move(Vector3 from, Vector3 to, float maxHeight)
    {
        if (tMove != null)
            tMove.Kill();

        body.position = from;
        body.SetActive(true);
        SetupRenderWarning(to);
        MoveTween(to);
    }
    private void MoveTween(Vector3 targetPoint)
    {
        float length = Vector3.Distance(body.position, targetPoint);
        float totalTime = length / speed;
        var sequence = DOTween.Sequence();
        sequence.Append(body.DOJump(targetPoint, forceShootMultiplier * totalTime, 1, totalTime).OnComplete(Explode).SetEase(yCurve))
                .Join(DOVirtual.Float(0f, 1f, totalTime, (ratio) =>
                {
                    renderWarning.color = colorWarning.Evaluate(ratio);
                }));

        tMove = sequence;
    }

    private void SetupRenderWarning(Vector3 position)
    {
        renderWarning.transform.position = position + Vector3.up * 0.01f;
        renderWarning.size = extents * 2f;
    }

    public async void Explode()
    {
        body.SetActive(false);
        fxExplosion.transform.position = body.position;
        fxExplosion.Play();
        onExpoded?.Invoke();
        renderWarning.DOFade(0f, 0.5f);

        Collider[] colliders = Physics.OverlapBox(body.position, extents);
        if (colliders == null || colliders.Length <= 0)
        {

        }
        else
        {
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IReceiveDamage receiver))
                {
                    receiver.ReceiveDamage(transform);
                    receiver.ReceiveForce(Vector3.up * forceAffectMC);
                }
            }
        }

        await UniTask.WaitForSeconds(delayReturn);
        onReturnPool?.Invoke();
    }

    public void SetOnReturnPool(Action callback)
    {
        onReturnPool = callback;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (renderWarning)
        {
            renderWarning.size = Vector3.one * affectZone * 2f;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 size = Vector3.one * affectZone * 2f;
        size.y = 0f;
        Gizmos.DrawWireCube(body.position, size);
    }
#endif
}