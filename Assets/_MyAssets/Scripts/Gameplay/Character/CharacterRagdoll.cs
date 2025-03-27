using MLib;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterRagdoll : MonoBehaviour
{
    [SerializeField] private bool enableOnAwkae = false;
    [SerializeField] private float multiplyMass = 1f;
    [SerializeField] private Transform rootIK = default;
    public Transform RootIK { get { return rootIK; } }

    private Rigidbody[] rigidbodys;
    private Collider[] colliders;

    private Vector3[] originPoses;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        rigidbodys = GetComponentsInChildren<Rigidbody>(true);
        colliders = GetComponentsInChildren<Collider>(true);
        originPoses = new Vector3[rigidbodys.Length];

        for (int i = 0; i < rigidbodys.Length; i++)
        {
            originPoses[i] = rigidbodys[i].transform.localPosition;
            rigidbodys[i].mass *= multiplyMass;
        }

        SetActiveRagdoll(enableOnAwkae);
    }

    public void AddForce(Vector3 force)
    {
        SetActiveRagdoll(true);
        for (int i = 0; i < rigidbodys.Length; i++)
        {
            rigidbodys[i].AddForce(force, ForceMode.Impulse);
        }
    }

    [Button]
    public void SetActiveRagdoll(bool b)
    {
        for (int i = 0; i < rigidbodys.Length; i++)
        {
            rigidbodys[i].isKinematic = !b;
            if (!rigidbodys[i].isKinematic)
                rigidbodys[i].linearVelocity = Vector3.zero;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = b;
        }

        animator.enabled = !b;

        if (b == false)
            ResetPos();
    }

    public void ResetPos()
    {
        for (int i = 0; i < rigidbodys.Length; i++)
        {
            rigidbodys[i].transform.localPosition = originPoses[i];
        }
    }

    [MButton]
    private void ActiveRagdoll()
    {
        SetActiveRagdoll(true);
    }

#if UNITY_EDITOR
    [Button]
    private void SetMass(float mass)
    {
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rig in rigids) { rig.mass = mass; }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}