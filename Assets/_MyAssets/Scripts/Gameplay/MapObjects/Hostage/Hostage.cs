using System;
using UnityEngine;

public class Hostage : InteractSpecial
{
    [Header("Context")]
    [SerializeField] private bool isReleaseable = false;
    [SerializeField] private Animator animator;
    [SerializeField] private MCDetector mcDetector;

    private int paramRelease = Animator.StringToHash("release");
    private int paramIsReleased = Animator.StringToHash("isReleased");
    private bool isFreedom = false;
    public bool IsFreedom => isFreedom;
    public static Action OnRelease;

    protected override void OnEnable()
    {
        base.OnEnable();
        mcDetector.Register_McEnter(ShowInteractGUI);
        mcDetector.Register_McExit(HideInteractGUI);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        mcDetector.Unregister_McEnter(ShowInteractGUI);
        mcDetector.Unregister_McExit(HideInteractGUI);
    }

    private void Start()
    {
        mcDetector.StartScan();
    }
    public void Releaseable()
    {
        isReleaseable = true;
    }
    protected override void OnTap()
    {
        base.OnTap();
        Release();
    }
    protected override bool CheckInteractable()
    {
        return !isFreedom && isReleaseable;
    }
    private void Release()
    {
        if (CheckInteractable() == false)
            return;

        isFreedom = true;
        animator.SetBool(paramIsReleased, true);
        animator.SetTrigger(paramRelease);
        OnRelease?.Invoke();

        HideInteractGUI();
    }
}