
using UnityEngine;

public class Prison : InteractTap
{
    [SerializeField] private SOPrisonKeyReference keyStorage;
    [SerializeField] private Hostage hostage;
    [SerializeField] private SpriteRenderer colorRenderer;
    [SerializeField] private Animation animOpen;
    [SerializeField] private BoxCollider col;
    [SerializeField] private MCDetector mCDetector;

    private bool isUnlocked;

    protected override void OnTap()
    {
        if (keyStorage.TryUnlockPrison(this))
        {
            Unlock();
            DoAnimUnlock();

            HideInteractGUI();
            mcDetector.StopScan();
        }
        else
        {
        }
    }
    protected override bool CheckInteractable()
    {
        bool hasKey = keyStorage.CheckContainCorrectKey(this);
        return hasKey && !isUnlocked;
    }
    private void DoAnimUnlock()
    {
        animOpen.Play();
    }
    private void Unlock()
    {
        hostage.transform.parent = transform.parent;
        hostage.Releaseable();
        hostage.Release();
        isUnlocked = true;
        col.enabled = false;
    }
    public void SetColor(Color color)
    {
        color.a = colorRenderer.color.a;
        colorRenderer.color = color;
    }
}