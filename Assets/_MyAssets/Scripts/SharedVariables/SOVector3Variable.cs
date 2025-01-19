
using UnityEngine;

[CreateAssetMenu(fileName = "SOVector3Variable", menuName = "SharedVariables/SOVector3Variable")]
public class SOVector3Variable: ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, TextArea] private string description;
#endif

    [MLib.ReadOnly] public Vector3 Value;
}