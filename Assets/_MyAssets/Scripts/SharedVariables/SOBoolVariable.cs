
using UnityEngine;

[CreateAssetMenu(fileName = "SOBoolVariable", menuName = "SharedVariables/SOBoolVariable")]
public class SOBoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    [MLib.ReadOnly] public bool Value;
}
