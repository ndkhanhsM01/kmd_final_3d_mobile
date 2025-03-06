
using System;
using UnityEngine;

public class Room: MonoBehaviour
{
    [SerializeField] private Collider colliderCamera;

    public static Action<Room> OnChangedRoom;

    public Collider ColliderCamera => colliderCamera;
    public void Show()
    {
        OnChangedRoom?.Invoke(this);
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}