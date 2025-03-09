
using Cysharp.Threading.Tasks;
using MLib;
using UnityEngine;

public class Gate : MonoBehaviour, ITriggerable
{
    [SerializeField] private Room owner;
    [SerializeField] private float pointForward = 1.5f;
    [SerializeField] private Transform arrowGraphic;

    private Level level => GameplayController.Instance.CurLevel;
    public Room RoomOwner => owner;

    private void OnValidate()
    {
        if(arrowGraphic)
            arrowGraphic.position = GetAppearPosition();
    }

    private void Start()
    {
        if (arrowGraphic)
            arrowGraphic.position = GetAppearPosition();
    }

    public void Trigger(Transform source)
    {
        Debug.Log("gate: " + source.name);

        if(source.TryGetComponent(out MCActionHandler mcAction))
        {
            ComeIn(mcAction);
        }
    }

    private async void ComeIn(MCActionHandler mcAction)
    {
        Gate partner = level.GatePairStorage.GetPartner(this);

        Vector3 appearMCPosition = partner.GetAppearPosition();
        mcAction.SetMotion(false);

        await UniTask.WaitForSeconds(0.5f);

        owner.Hide();
        partner.RoomOwner.Show();
        mcAction.SetPosition(appearMCPosition);
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