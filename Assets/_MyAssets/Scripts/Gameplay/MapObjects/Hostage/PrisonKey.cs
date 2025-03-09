
using DG.Tweening;
using MLib;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PrisonKey : MonoBehaviour, ITriggerable
{
    [SerializeField] private SOPrisonKeyReference storage;
    [SerializeField] private SpriteRenderer colorRenderer;

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
    }
    public Color GetColor()
    {
        return colorRenderer.color;
    }

    [Button]
    public void DoAnimPickup()
    {
        bodyGraphic.DOKill();
        Camera camera = Camera.main;
        Vector3 lookDir = (camera.transform.position - bodyGraphic.position).normalized;
        float beginScale = bodyGraphic.localScale.x;
        Quaternion rotationTarget = Quaternion.LookRotation(lookDir, -Vector3.right) * Quaternion.Euler(Vector3.right * 90f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(bodyGraphic.DOMoveY(bodyGraphic.position.y + 4f, 0.75f))
                .Join(bodyGraphic.DORotateQuaternion(rotationTarget, 0.25f))
                .Append(bodyGraphic.DOScale(beginScale * 1.1f, 0.07f))
                .Append(bodyGraphic.DOScale(0f, 0.15f));

        sequence.OnComplete(() => gameObject.SetActive(false));
    }
}