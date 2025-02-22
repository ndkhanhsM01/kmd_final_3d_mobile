using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MCDetector : MonoBehaviour
{
    [SerializeField] private bool scanOnStart = false;
    [SerializeField] private float radius = 1f;

    [SerializeField] private UnityEvent onMcEnter;
    [SerializeField] private UnityEvent onMcExit;
    [SerializeField] private UnityEvent onMcStay;

    private bool isScaning;
    private bool isMcStay;
    private Coroutine crScan;
    private void Start()
    {
        if (scanOnStart)
            StartScan();
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

    public void StartScan()
    {
        StopScan();
        crScan = StartCoroutine(IE_ScanMC());
        isScaning = true;
    }

    public void StopScan()
    {
        if (crScan != null)
            StopCoroutine(crScan);
        isScaning = false;
    }

    private IEnumerator IE_ScanMC()
    {
        while (!GameplayController.Instance.MC)
        {
            Debug.LogWarning("MC not found");
            yield return null;
        }

        Transform mc = GameplayController.Instance.MC.Body;
        Transform body = transform;
        while (true)
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