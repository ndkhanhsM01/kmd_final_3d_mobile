
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioInvoker: MonoBehaviour
{
    [SerializeField] private float durationRequire = -1;
    [SerializeField] private SOAudio config;

    [Button]
    public void PlayAudio()
    {
        config.Play(durationRequire);
    }
}