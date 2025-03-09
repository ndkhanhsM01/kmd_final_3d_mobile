
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Room: MonoBehaviour
{
    [SerializeField] private BoxCollider colliderCamera;

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

    [Button]
    private void SetDefaultValuesCollider()
    {
        colliderCamera.center = new Vector3(0f, 3.5f, -9.5f);
        colliderCamera.size = new Vector3(20, 30, 4.5f);
    }
}