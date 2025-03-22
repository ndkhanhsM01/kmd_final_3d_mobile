using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MCDetector : MonoBehaviour
{

    [SerializeField, ReadOnly] private bool isScaning;
    [SerializeField] private bool scanOnStart = false;
    [SerializeField] private float radius = 1f;

    [SerializeField] private UnityEvent onMcEnter;
    [SerializeField] private UnityEvent onMcExit;
    [SerializeField] private UnityEvent onMcStay;
    private bool isMcStay;
    private Coroutine crScan;
    public bool IsMcStay => isMcStay;
    private void Start()
    {
        if (scanOnStart)
            StartScan();
    }
    private void OnEnable()
    {
        if (isScaning)
            StartScan();
    }
    private void OnDisable()
    {
        if (crScan != null)
            StopCoroutine(crScan);
    }
    public void Register_McEnter(UnityAction callback)
    {
        onMcEnter.AddListener(callback);
    }
    public void Register_McExit(UnityAction callback)
    {
        onMcExit.AddListener(callback);
    }
    public void Register_McStay(UnityAction callback)
    {
        onMcStay.AddListener(callback);
    }
    public void Unregister_McEnter(UnityAction callback)
    {
        onMcEnter.RemoveListener(callback);
    }
    public void Unregister_McExit(UnityAction callback)
    {
        onMcExit.RemoveListener(callback);
    }
    public void Unregister_McStay(UnityAction callback)
    {
        onMcStay.RemoveListener(callback);
    }
    public void ClearAllListeners()
    {
        onMcEnter.RemoveAllListeners();
        onMcExit.RemoveAllListeners();
    }

    [Button]
    public void StartScan()
    {
        StopScan();
        isScaning = true;
        crScan = StartCoroutine(IE_ScanMC());
    }

    [Button]
    public void StopScan()
    {
        isScaning = false;
        if (crScan != null)
            StopCoroutine(crScan);
        isMcStay = false;
    }

    private IEnumerator IE_ScanMC()
    {
        while (!GameplayController.MC)
        {
            Debug.LogWarning("MC not found");
            yield return null;
        }

        if (GameplayController.MC.Body == null)
        {
            yield return new WaitUntil(() => GameplayController.MC.Body);
        }


        Transform mc = GameplayController.MC.Body;
        Transform body = transform;
        while (isScaning && mc)
        {
            float distance = Vector3.Distance(body.position, mc.position);

            if (distance <= radius && !isMcStay)
            {
                isMcStay = true;
                onMcEnter?.Invoke();
            }
            else if (distance > radius && isMcStay)
            {
                isMcStay = false;
                onMcExit?.Invoke();
            }
            else if(distance <= radius && isMcStay)
            {
                onMcStay?.Invoke();
            }

            yield return null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isScaning ? Color.green : Color.yellow;
        DebugUtil.DrawGizmoArc(transform.position, transform.forward, transform.up, radius, 360f);
    }
#endif
}