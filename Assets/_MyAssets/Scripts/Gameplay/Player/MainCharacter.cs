using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soDefaultStats.Radius);
    }
#endif
}
