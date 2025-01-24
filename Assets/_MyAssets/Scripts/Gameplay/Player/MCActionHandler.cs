

using MLib;
using UnityEngine;

public class MCActionHandler: MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Header("Values")]
    [SerializeField] private SOMcDefaultStats stats;
    [SerializeField] private SOVector3Variable moveDirectionVar;

    [Header("Channels")]
    [SerializeField] private SOBoolEventChannel freezeGameChannel;

    private bool isFreezeGame = false;

    private void OnEnable()
    {
        freezeGameChannel.Register(OnFreezeGame);
    }
    private void OnDisable()
    {
        freezeGameChannel.Unregister(OnFreezeGame);
    }

    private void FixedUpdate()
    {
        if (isFreezeGame) 
            return;

        CaculateMove();
    }
    private void CaculateMove()
    {
        Vector3 direction = moveDirectionVar.Value;
        rb.linearVelocity = direction * stats.MoveSpeed * Time.fixedDeltaTime;

        if (direction != Vector3.zero)
        {
            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(direction), stats.TurnSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnFreezeGame(bool status)
    {
        isFreezeGame = status;
        rb.isKinematic = status;

        rb.linearVelocity = Vector3.zero;
    }
}