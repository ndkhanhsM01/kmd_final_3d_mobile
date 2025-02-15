
using MLib;
using UnityEngine;

public class PrisonKey : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOPrisonKeyReference storage;
    [SerializeField] private SpriteRenderer colorRenderer;
    public void Trigger(Transform interaction)
    {
        storage.AddKey(this);
        this.SetActive(false);
    }
    public void SetColor(Color color)
    {
        color.a = colorRenderer.color.a;
        colorRenderer.color = color;
    }
}