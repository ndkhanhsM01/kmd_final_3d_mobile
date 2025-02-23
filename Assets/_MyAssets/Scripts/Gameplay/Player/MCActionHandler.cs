using MLib;
using UnityEngine;
using AnimatorParam = MainCharacter.AnimatorParam;

public class MCActionHandler: MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterRagdoll ragdoll;

    [Header("Values")]
    [SerializeField] private SOMcDefaultStats stats;
    [SerializeField] private SOVector3Variable moveDirectionVar;

    [Header("Channels")]
    [SerializeField] private SOBoolEventChannel freezeGameChannel;
    [SerializeField] private SOVector3EventChannel pushMcChannel;

    private bool isFreezeGame = false;

    private float y;
    private float targetSpeed;
    [SerializeField, ReadOnly] private float finalSpeed;

    public bool ActiveMoveAround { get; set; }
    private void Awake()
    {
        y = rb.position.y;
        ActiveMoveAround = true;
    }
    private void OnEnable()
    {
        freezeGameChannel.Register(OnFreezeGame);
        pushMcChannel.Register(OnReceiveForce);
    }
    private void OnDisable()
    {
        freezeGameChannel.Unregister(OnFreezeGame);
        pushMcChannel.Unregister(OnReceiveForce);
    }

    private void FixedUpdate()
    {
        if (isFreezeGame) 
            return;

        if (ActiveMoveAround)
        {
            CaculateMove();
            UpdateAnimation();
        }
    }
    private void CaculateMove()
    {
        Vector3 direction = moveDirectionVar.Value;
        targetSpeed = direction.magnitude * stats.MoveSpeed;

        finalSpeed = Mathf.Lerp(finalSpeed, targetSpeed, stats.MoveAcceleration * Time.fixedDeltaTime);
        direction.Normalize();
        if (!rb.isKinematic)
            rb.linearVelocity = direction * finalSpeed * Time.fixedDeltaTime;

        if (direction != Vector3.zero)
        {
            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(direction), stats.TurnSpeed * Time.fixedDeltaTime);
        }
    }
    private void UpdateAnimation()
    {
        float ratioCurSpeed = finalSpeed / stats.MoveSpeed;
        animator.SetFloat(AnimatorParam.moveSpeed, ratioCurSpeed);
    }
    private void OnFreezeGame(bool status)
    {
        isFreezeGame = status;
        SetMotion(!isFreezeGame);
    }
    private void OnReceiveForce(Vector3 force)
    {
        SetMotion(false);
        ragdoll.AddForce(force);
    }
    public void SetMotion(bool active)
    {
        if(!active)
            rb.linearVelocity = Vector3.zero;

        rb.isKinematic = !active;
    }
    public void SetPosition(Vector3 position)
    {
        SetMotion(false);
        position.y = y;
        rb.position = position;
        SetMotion(true);
    }
    public void ReceiveForce(Vector3 force)
    {
        OnReceiveForce(force);
    }
}