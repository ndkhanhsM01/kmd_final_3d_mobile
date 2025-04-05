using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    [SerializeField] private Transform model;
    [SerializeField] private SOAudio audioHandle;

    [Header("setting")]
    [SerializeField] private float yOpen = -3f;
    [SerializeField] private float durationTransition = 1.2f;

    private Tween tMove;

    [Button]
    public void Open()
    {
        DoTransition(yOpen).OnComplete(() =>
        {
            hitbox.SetActive(false);
        });
    }

    [Button]
    public void Close()
    {
        hitbox.SetActive(true);
        DoTransition(0f);
    }

    private Tween DoTransition(float y)
    {
        if (tMove != null)
            tMove.Kill();

        tMove = model.DOLocalMoveY(y, durationTransition);
        audioHandle.Play(durationTransition * 0.95f);
        return tMove;
    }
}
