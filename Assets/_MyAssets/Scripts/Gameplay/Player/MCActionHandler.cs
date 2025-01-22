

using MLib;
using UnityEngine;

public class MCActionHandler: MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private SOMcDefaultStats soDefaultStats;
    [SerializeField] private SOVector3Variable moveDirectionVar;

    private void FixedUpdate()
    {
        CaculateMove();

    }
    private void CaculateMove()
    {
        transform.Translate(moveDirectionVar.Value * soDefaultStats.MoveSpeed * Time.fixedDeltaTime);
        //characterController.Move(moveDirectionVar.Value * soDefaultStats.MoveSpeed * Time.fixedDeltaTime);
    }
}