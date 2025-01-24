
using UnityEngine;

public class Gate : MonoBehaviour, ITriggerable
{
    [SerializeField] private Vector3 pointAppear = Vector3.forward;

    private Level level => GameplayController.Instance.CurLevel;
    public void Trigger()
    {
        ComeIn();
    }

    private void ComeIn()
    {
        Gate partner = level.GatePairStorage.GetPartner(this);

        Vector3 appearMCPosition = partner.GetAppearPosition();
    }

    public Vector3 GetAppearPosition()
    {
        return transform.position + pointAppear;
    }
}