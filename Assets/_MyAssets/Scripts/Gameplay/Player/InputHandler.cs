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
        sharedMoveDirection.Value.x = joystick.Horizontal;
        sharedMoveDirection.Value.z = joystick.Vertical;
    }

    private void OnTapSpecial()
    {
        Debug.Log("Tap");
    }
    private void OnBeginHoldSpecial() 
    {
        Debug.Log("Begin hold");
    }
    private void OnEndHoldSpecial()
    {
        Debug.Log("End hold");
    }
    private void OnReleasedSpecial()
    {
        Debug.Log("Released");
    }
}
