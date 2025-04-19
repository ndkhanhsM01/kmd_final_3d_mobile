using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectMoveable : MonoBehaviour
{
    [Header("Line renderer")]
    [SerializeField] private float yLine = 0.05f;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Config")]
    [SerializeField] private bool lookForward = true;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float delayMove = 0f;
    [SerializeField] private Transform[] points;

    private int curIndexPoint;
    private Transform body;
    private float timeWait = 0f;

    private void Awake()
    {
        body = transform;
        curIndexPoint = 0;
        timeWait = delayMove;
    }
    [Button]
    private void SetUpLine()
    {
        if (!lineRenderer || points == null) return;

        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 position = points[i].localPosition;
            position.y = yLine;
            lineRenderer.SetPosition(i, position);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    private void Update()
    {
        if (timeWait > 0f)
        {
            timeWait -= Time.deltaTime;
            return;
        }
        Transform target = GetCurPoint();
        Vector3 direction = (target.position - body.position).normalized;
        Vector3 step = direction * moveSpeed * Time.deltaTime;

        body.position += step;

        if(lookForward && direction != Vector3.zero)
            body.rotation = Quaternion.Lerp(body.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        if(Vector3.Distance(target.position, body.position) <= moveSpeed * Time.deltaTime)
        {
            curIndexPoint = (curIndexPoint+1) % points.Length;
        }
    }

    private Transform GetCurPoint()
    {
        return points[curIndexPoint];
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (points != null && points.Length < 2) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 point1 = points[i].position;
            Vector3 point2 = points[(i + 1) % points.Length].position;
            Gizmos.DrawWireSphere(point1, 0.2f);
            Gizmos.DrawLine(point1, point2);
        }
    }
#endif
}
