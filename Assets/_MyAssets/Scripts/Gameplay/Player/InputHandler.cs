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

    private void Update()
    {
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
}
