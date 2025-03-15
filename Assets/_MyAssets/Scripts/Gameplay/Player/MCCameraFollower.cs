
using Unity.Cinemachine;
using UnityEngine;

public class MCCameraFollower: MonoBehaviour
{
    [SerializeField] private CinemachineBrain camBrain;
    [SerializeField] private CinemachineConfiner3D confiner3D;

    public static CinemachineBrain CamBrain { get; private set; }

    private void Awake()
    {
        CamBrain = camBrain;
    }
    private void OnEnable()
    {
        Room.OnChangedRoom += OnChangedRoom;
    }
    private void OnDisable()
    {
        Room.OnChangedRoom -= OnChangedRoom;
    }

    private void OnChangedRoom(Room newRoom)
    {
        confiner3D.BoundingVolume = newRoom.ColliderCamera;
    }
}