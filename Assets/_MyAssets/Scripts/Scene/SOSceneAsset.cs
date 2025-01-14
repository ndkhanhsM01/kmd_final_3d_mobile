using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSceneAsset", menuName = "Others/SOSceneAsset")]
public class SOSceneAsset : ScriptableObject
{
    [SerializeField] private byte index;
    [SerializeField] private bool isCommon;
    [SerializeField] private SOVoidEventChannel readyChannel;

    public byte Index => index;
    public bool IsCommon => isCommon;
    public SOVoidEventChannel ReadyChannel => readyChannel;
}
