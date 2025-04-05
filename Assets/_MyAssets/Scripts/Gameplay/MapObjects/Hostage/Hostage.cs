using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Windows.WebCam.VideoCapture;

public class Hostage : InteractTap
{
    [Header("Context")]
    [SerializeField] private bool isReleaseable = false;
    [SerializeField] private UnityEvent evtRelease;
    [SerializeField] private SOAudio audioSave;

    private bool isFreedom = false;
    public bool IsFreedom => isFreedom;
    public static Action OnRelease;
    public void Releaseable()
    {
        isReleaseable = true;
        mcDetector.StartScan();
    }
    protected override void OnTap()
    {
        Release();
    }
    protected override bool CheckInteractable()
    {
        return !isFreedom && isReleaseable;
    }
    public void Release()
    {
        if (CheckInteractable() == false)
            return;

        isFreedom = true;
        OnRelease?.Invoke();
        evtRelease?.Invoke();
        audioSave.Play();

        HideInteractGUI();
        mcDetector.StopScan();
    }
}