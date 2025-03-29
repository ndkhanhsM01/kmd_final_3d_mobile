
using DG.Tweening;
using MLib;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PrisonKey : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOPrisonKeyReference storage;
    [SerializeField] private SpriteRenderer colorRenderer;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private byte outlineSlot = 1;

    [SerializeField] private Transform bodyGraphic;
    [SerializeField] private UnityEvent evtPickup;
    public void Trigger(Transform source)
    {
        evtPickup?.Invoke();
        storage.AddKey(this);
        DoAnimPickup();
        //this.SetActive(false);
    }
    public void SetColor(Color color)
    {
        color.a = colorRenderer.color.a;
        colorRenderer.color = color;
        mesh.materials[outlineSlot].SetColor("_Outline_Color", color);
    }
    public Color GetColor()
    {
        return colorRenderer.color;
    }

    [Button]
    public void DoAnimPickup()
    {
        bodyGraphic.DoAnimPickup(4f, () =>
        {
            gameObject.SetActive(false);
        });
    }
}