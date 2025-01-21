

using UnityEngine;

public class MCActionHandler: MonoBehaviour
{
    [SerializeField] private SOMcDefaultStats soDefaultStats;
    [SerializeField] private SOVector3Variable moveDirectionVar;

    private void FixedUpdate()
    {
        transform.Translate(moveDirectionVar.Value * soDefaultStats.MoveSpeed * Time.fixedDeltaTime);
    }
}