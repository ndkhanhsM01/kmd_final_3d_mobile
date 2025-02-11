using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public static class AnimatorParam
    {
        public static int moveSpeed = Animator.StringToHash("moveSpeed");
    }

    [SerializeField] private SOMcDefaultStats soDefaultStats;
    [SerializeField] private MCActionHandler actionHandler;
    [SerializeField] private MCInteraction interaction;

    public Transform Body { get; private set; }
    public float Radius => soDefaultStats.Radius;
    public MCActionHandler Action => actionHandler;
    private void Awake()
    {
        Body = transform;
    }

    public void SaveHostage(Hostage target)
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soDefaultStats.Radius);
    }
#endif
}
