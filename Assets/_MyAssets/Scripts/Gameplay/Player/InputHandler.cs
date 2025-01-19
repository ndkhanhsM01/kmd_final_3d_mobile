using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private SpecialSelection specialSelection;

    [Space(20f)]
    [Header("Shared variable")]
    [SerializeField] private SOVector3Variable sharedMoveDirection;

    private void Update()
    {
        sharedMoveDirection.Value.x = joystick.Horizontal;
        sharedMoveDirection.Value.y = joystick.Vertical;
    }
}
