using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private SOMcDefaultStats soDefaultStats;
    [SerializeField] private MCActionHandler actionHandler;
    [SerializeField] private MCInteraction interaction;

    public Transform Body { get; private set; }
    private void Awake()
    {
        Body = transform;
    }
}
