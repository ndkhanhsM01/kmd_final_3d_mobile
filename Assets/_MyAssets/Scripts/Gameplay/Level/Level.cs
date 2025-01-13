using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MainCharacter mc;
    [SerializeField] private LevelViewport viewport;

    public MainCharacter MC => mc;
}
