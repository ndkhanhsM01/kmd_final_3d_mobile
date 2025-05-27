using MLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacter : MonoBehaviour, IReceiveDamage
{
    public static class AnimatorParam
    {
        public static int moveSpeed = Animator.StringToHash("moveSpeed");
    }

    [SerializeField] private SOBoolVariable godStatus;
    [SerializeField] private SOMcDefaultStats soDefaultStats;
    [SerializeField] private SOMCState state;
    [SerializeField] private MCActionHandler actionHandler;
    [SerializeField] private MCInteraction interaction;
    [SerializeField] private CharacterSkin skin;
    [SerializeField] private ShieldEquipment equipment;
    [SerializeField] private GodStatusHandler godStatusHandler;

    [Header("Events")]
    [SerializeField] private SOBoolEventChannel setEquipShieldChannel;
    [SerializeField] private SOVoidEventChannel reviveChannel;
    [SerializeField] private SOVoidEventChannel sceneLoadedChannel;

    [Header("Debug")]
    [SerializeField] private EditorConfigSO testSO;

    public Transform Body { get; private set; }
    public float Radius => soDefaultStats.Radius;
    public MCActionHandler Action => actionHandler;
    private void Awake()
    {
        Body = transform;
        godStatus.Value = false;
        state.Current = MCState.Alive;
    }
    private void OnEnable()
    {
        reviveChannel.Register(Revive);
    }
    private void OnDisable()
    {
        reviveChannel.Unregister(Revive);
    }
    public void SaveHostage(Hostage target)
    {

    }
    public void Revive()
    {
        GameplayController.Instance.SetFreezeGame(false);
        actionHandler.SetMotion(true);
        actionHandler.SetActiveRagdoll(false);
        godStatusHandler.Active(0f);
        state.Current = MCState.Alive;
    }
    public void ReceiveForce(Vector3 force)
    {
        actionHandler.ReceiveForce(force);
    }
    public bool ReceiveDamage(Transform source)
    {
        return TryDeath();
    }
    public bool TryDeath()
    {
#if UNITY_EDITOR
        if (testSO.CheatGodMode)
        {
            return false;
        }
#endif

        if (godStatus.Value)
        {
            setEquipShieldChannel.Raise(false);
            return false;
        }
        else
        {
            GameplayController.Instance.LoseLevelDelay(0.75f);
            state.Current = MCState.Dead;
            return true;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soDefaultStats.Radius);
    }
#endif
}
