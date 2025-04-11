
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class ItemLookAtCam: MonoBehaviour
{
    [SerializeField] private LookAtConstraint lookAtConstraint;

    [InfoBox("Default Camera reference is Main Camera")]
    [SerializeField] private Camera specialCam;

    private Camera camReference;

    private void Reset()
    {
        lookAtConstraint = GetComponent<LookAtConstraint>();
    }
    private void OnEnable()
    {
        camReference = specialCam ? specialCam : Camera.main;

        ConstraintSource source = new();
        source.weight = 1;
        source.sourceTransform = camReference.transform;
        lookAtConstraint.SetSources(new() { source });

        lookAtConstraint.constraintActive = true;
    }
}