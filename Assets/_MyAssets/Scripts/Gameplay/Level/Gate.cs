
using Cysharp.Threading.Tasks;
using MLib;
using UnityEngine;

public class Gate : MonoBehaviour, ITriggerable
{
    [SerializeField] private Room owner;
    [SerializeField] private float pointForward = 1.5f;

    private Level level => GameplayController.Instance.CurLevel;
    public Room RoomOwner => owner;
    public void Trigger(Transform interaction)
    {
        ComeIn();
    }

    private async void ComeIn()
    {
        Gate partner = level.GatePairStorage.GetPartner(this);

        Vector3 appearMCPosition = partner.GetAppearPosition();
        GameplayController.Instance.MC.Action.SetMotion(false);

        await UniTask.WaitForSeconds(0.5f);

        owner.SetActive(false);
        partner.RoomOwner.SetActive(true);
        GameplayController.Instance.MC.Action.GoTo(appearMCPosition);
    }

    public Vector3 GetAppearPosition()
    {
        return transform.position + transform.forward * pointForward;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetAppearPosition(), 0.2f);
    }
#endif
}