using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider hitbox;
    [SerializeField] private Transform model;

    [Header("setting")]
    [SerializeField] private float yOpen = -3f;
    [SerializeField] private float durationTransition = 1.2f;

    private Tween tMove;

    [Button]
    public void Open()
    {
        DoTransition(yOpen).OnComplete(() =>
        {
            hitbox.enabled = false;
        });
    }

    [Button]
    public void Close()
    {
        hitbox.enabled = true;
        DoTransition(0f);
    }

    private Tween DoTransition(float y)
    {
        if (tMove != null)
            tMove.Kill();

        tMove = model.DOLocalMoveY(y, durationTransition);

        return tMove;
    }
}
