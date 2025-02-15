
using UnityEngine;

public class Prison : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOPrisonKeyReference keyStorage;
    [SerializeField] private Hostage hostage;
    [SerializeField] private SpriteRenderer colorRenderer;

    public void Trigger(Transform interaction)
    {
        if (keyStorage.TryUnlockPrison(this))
        {
            Unlock();
            gameObject.SetActive(false);
        }
        else
        {
        }
    }
    private void Unlock()
    {
        hostage.transform.parent = transform.parent;
        hostage.Releaseable();
    }
    public void SetColor(Color color)
    {
        color.a = colorRenderer.color.a;
        colorRenderer.color = color;
    }
}