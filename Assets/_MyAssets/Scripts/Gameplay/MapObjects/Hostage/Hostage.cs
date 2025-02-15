using System;
using UnityEngine;

public class Hostage : MonoBehaviour, ITriggerable
{
    [SerializeField] private bool isReleaseable = false;
    [SerializeField] private Animator animator;

    private int paramRelease = Animator.StringToHash("release");
    private int paramIsReleased = Animator.StringToHash("isReleased");
    private bool isFreedom = false;
    public bool IsFreedom => isFreedom;
    public static Action OnRelease;

    public void Releaseable()
    {
        isReleaseable = true;
    }

    public void Trigger(Transform interaction)
    {
        if (IsFreedom || !isReleaseable)
            return;

        Release();
    }
    private void Release()
    {
        isFreedom = true;
        animator.SetBool(paramIsReleased, true);
        animator.SetTrigger(paramRelease);
        OnRelease?.Invoke();
    }
}