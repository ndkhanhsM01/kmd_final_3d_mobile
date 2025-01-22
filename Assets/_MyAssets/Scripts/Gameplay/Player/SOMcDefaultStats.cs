using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOMcDefaultStats", menuName = "Gameplay/SOMcDefaultStats")]
public class SOMcDefaultStats : ScriptableObject
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveAcceleration = 3f;
    [SerializeField] private float turnSpeed = 10f;


    #region public get values
    public float Radius => radius;
    public float MoveSpeed => moveSpeed;
    public float MoveAcceleration => moveAcceleration;
    public float TurnSpeed => turnSpeed;
    #endregion
}
