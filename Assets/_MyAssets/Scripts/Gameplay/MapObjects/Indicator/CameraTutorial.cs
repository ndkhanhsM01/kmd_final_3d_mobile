
using Cysharp.Threading.Tasks;
using MLib;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraTutorial: MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private float delayFocus = 0.5f;
    [SerializeField] private float focusDuration = -1f;

    [SerializeField, FoldoutGroup("Events")] private UnityEvent evtFocusComplete;
    [SerializeField, FoldoutGroup("Events")] private UnityEvent evtUnfocusComplete;

    private void Awake()
    {
        cam.SetActive(false);
    }

    public async void Focus()
    {
        GameplayController.Instance.SetFreezeGame(true);

        await UniTask.WaitForSeconds(0.5f);
        cam.SetActive(true);

        await UniTask.WaitUntil(() => MCCameraFollower.CamBrain.IsBlending == false);

        evtFocusComplete?.Invoke();

        if (focusDuration > 0f)
        {
            await UniTask.WaitForSeconds(focusDuration);
            Unfocus();
        }

    }

    public async void Unfocus()
    {
        cam.SetActive(false);

        await UniTask.WaitUntil(() => MCCameraFollower.CamBrain.IsBlending == false);
        evtUnfocusComplete?.Invoke();

        GameplayController.Instance.SetFreezeGame(false);
    }
}