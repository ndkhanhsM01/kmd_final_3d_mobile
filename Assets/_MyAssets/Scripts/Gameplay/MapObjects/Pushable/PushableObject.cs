using MLib;
using UnityEngine;

public class PushableObject : ObjectInteractable
{
    [Space(20f)]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform arrow;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private SOVector3Variable mcMoveDirection;

    [SerializeField, ReadOnly] private Vector3 moveDirection;
    private Transform body;
    private bool isPushing;

    private RigidbodyConstraints rotationConst = RigidbodyConstraints.FreezeRotation;
    private RigidbodyConstraints defaultConst = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
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

        if(!isPushing )
        {
            CaculateDirectionMove();
            CaculateRigidConst();
            arrow.rotation = Quaternion.LookRotation(moveDirection);
        }
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
    private void CaculateRigidConst()
    {
        RigidbodyConstraints constraints = rotationConst;
        bool isVertical = moveDirection == Vector3.forward || moveDirection == Vector3.back;
        if (isVertical)
        {
            constraints = defaultConst | RigidbodyConstraints.FreezePositionX;
        }
        else
        {
            constraints = defaultConst | RigidbodyConstraints.FreezePositionZ;
        }
        rigid.constraints = constraints;
    }
    private void CaculateDirectionMove()
    {
        Vector3 point1 = new Vector3(mc.Body.position.x, 0f, mc.Body.position.z);
        Vector3 point2 = new Vector3(body.position.x, 0f, body.position.z);
        Vector3 dirToMC = point2 - point1;
        float angle = Mathf.Atan2(dirToMC.z, dirToMC.x) * Mathf.Rad2Deg;
        moveDirection = GetAlignDirection(angle);
    }
    private Vector3 GetAlignDirection(float angle)
    {
        if (angle >= -45f && angle < 45f)
            return Vector3.right;
        else if (angle >= 45f && angle < 135f)
            return Vector3.forward;
        else if ((angle >= 135f && angle < 180f) || (angle <= -135f && angle >= -180f))
            return Vector3.left;
        else
            return Vector3.back;
    }
}
