using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MCDetector : MonoBehaviour
{
    [SerializeField] private float radius = 1f;

    [SerializeField] private UnityEvent onMcEnter;
    [SerializeField] private UnityEvent onMcExit;

    private bool isScaning;
    private bool isMcStay;
    private Coroutine crScan;
    public void Register_McEnter(UnityAction callback)
    {
        onMcEnter.AddListener(callback);
    }
    public void Register_McExit(UnityAction callback)
    {
        onMcExit.AddListener(callback);
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
        if (!GameplayController.Instance.MC)
        {
            Debug.LogError("MC not found");
            yield break;
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