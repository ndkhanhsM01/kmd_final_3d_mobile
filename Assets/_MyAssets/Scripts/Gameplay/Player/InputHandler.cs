using MLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Space(10f)]
    [Header("Input")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private SpecialSelection specialSelection;

    [Space(10f)]
    [Header("Shared variable")]
    [SerializeField] private SOVector3Variable sharedMoveDirection;

    [Space(10f)]
    [Header("Events")]
    [SerializeField] private SOVoidEventChannel tapChannel;
    [SerializeField] private SOVoidEventChannel beginHoldChannel;
    [SerializeField] private SOVoidEventChannel endHoldChannel;

    [Space(10f)]
    [Header("Debug")]
    [SerializeField] private EditorConfigSO testingConfig;

    public bool ActiveMoveAround;
    private void Awake()
    {
        ActiveMoveAround = true;
    }
    private void OnEnable()
    {
        specialSelection.OnTap += OnTapSpecial;
        specialSelection.OnBeginHolding += OnBeginHoldSpecial;
        specialSelection.OnEndHolding += OnEndHoldSpecial;
        specialSelection.OnReleased += OnReleasedSpecial;
    }
    private void OnDisable()
    {
        specialSelection.OnTap -= OnTapSpecial;
        specialSelection.OnBeginHolding -= OnBeginHoldSpecial;
        specialSelection.OnEndHolding -= OnEndHoldSpecial;
        specialSelection.OnReleased -= OnReleasedSpecial;
    }

    private void Update()
    {
        if (!ActiveMoveAround)
        {
            sharedMoveDirection.Value = Vector3.zero;
            return;
        }
#if UNITY_EDITOR
        if (!testingConfig.IsUseJoystick)
        {
            sharedMoveDirection.Value.x = Input.GetAxis("Horizontal");
            sharedMoveDirection.Value.z = Input.GetAxis("Vertical");
            return;
        }
#endif

        sharedMoveDirection.Value.x = joystick.Horizontal;
        sharedMoveDirection.Value.z = joystick.Vertical;
    }

    private void OnTapSpecial()
    {
        tapChannel.Raise();
    }
    private void OnBeginHoldSpecial() 
    {
        beginHoldChannel.Raise();
    }
    private void OnEndHoldSpecial()
    {
        endHoldChannel.Raise();
    }
    private void OnReleasedSpecial()
    {
        //Debug.Log("Released");
    }
}
