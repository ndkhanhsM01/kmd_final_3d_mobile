using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public static class AnimatorParam
    {
        public static int moveSpeed = Animator.StringToHash("moveSpeed");
    }

    [SerializeField] private SOBoolVariable godStatus;
    [SerializeField] private SOMcDefaultStats soDefaultStats;
    [SerializeField] private MCActionHandler actionHandler;
    [SerializeField] private MCInteraction interaction;
    [SerializeField] private ShieldEquipment equipment;

    [Header("Events")]
    [SerializeField] private SOBoolEventChannel setEquipShieldChannel;

    public Transform Body { get; private set; }
    public float Radius => soDefaultStats.Radius;
    public MCActionHandler Action => actionHandler;
    private void Awake()
    {
        Body = transform;
        godStatus.Value = false;
    }

    public void SaveHostage(Hostage target)
    {

    }

    public bool TryDeath()
    {
        if (godStatus.Value)
        {
            setEquipShieldChannel.Raise(false);
            return false;
        }
        else
        {
            GameplayController.Instance.LoseLevelDelay(0.75f);
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
