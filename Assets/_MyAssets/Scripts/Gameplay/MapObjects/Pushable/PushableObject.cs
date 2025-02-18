using MLib;
using Unity.VisualScripting;
using UnityEngine;

public class PushableObject : ObjectInteractable
{
    [Space(20f)]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform[] pushPoints;
    [SerializeField] private SOVector3Variable mcMoveDirection;

    private Vector3 moveDirection;
    private Transform body;
    private Transform parentOfMc;
    private float angle;
    private void Awake()
    {
        body = transform;
    }
    protected override void Start()
    {
        base.Start();
        arrow.SetActive(false);
    }
    protected override void OnMC_StayZone()
    {
        base.OnMC_StayZone();

        CaculateDirectionMove();
        arrow.rotation = Quaternion.LookRotation(moveDirection);
    }
    protected override void OnMC_EnterZone()
    {
        base.OnMC_EnterZone();
        arrow.SetActive(true);
    }
    protected override void OnMC_ExitZone()
    {
        base.OnMC_ExitZone();
        arrow.SetActive(false);
    }
    protected override void OnBeginHold()
    {
        base.OnBeginHold();
        GameplayController.Instance.Input.ActiveMoveAround = false;
        //CaculateDirectionMove();
        parentOfMc = mc.Body.parent;
        Transform newParent = GetAlignPoint();
        mc.Body.parent = newParent;
        mc.transform.localPosition = new Vector3(0f, mc.Body.localPosition.y, 0f);
        mc.transform.rotation = Quaternion.LookRotation(moveDirection);
    }
    protected override void OnEndHold()
    {
        base.OnEndHold();
        GameplayController.Instance.Input.ActiveMoveAround = true;
        mc.Body.parent = parentOfMc;
    }
    protected override void OnHolding()
    {
        base.OnHolding();
        body.position += moveSpeed * Time.deltaTime * moveDirection;
    }
    private void CaculateDirectionMove()
    {
        Vector3 point1 = new Vector3(mc.Body.position.x, 0f, mc.Body.position.z);
        Vector3 point2 = new Vector3(body.position.x, 0f, body.position.z);
        Vector3 dirToMC = point2 - point1;
        angle = Mathf.Atan2(dirToMC.z, dirToMC.x) * Mathf.Rad2Deg;
        angle = GetAlignAngle(angle);
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        moveDirection = rotation * body.right;
        moveDirection.Normalize();
    }
    private float GetAlignAngle(float angle)
    {
        if (angle >= -45f && angle < 45f)
            return 0f;
        else if (angle >= 45f && angle < 135f)
            return 90f;
        else if ((angle >= 135f && angle < 180f) || (angle <= -135f && angle >= -180f))
            return 180f;
        else
            return -90f;
    }
    private Transform GetAlignPoint()
    {
        if (angle == 0f)
            return pushPoints[0];
        else if (angle == 90f)
            return pushPoints[1];
        else if (angle == 180f || angle == -180f)
            return pushPoints[2];
        else
            return pushPoints[3];
    }
}
