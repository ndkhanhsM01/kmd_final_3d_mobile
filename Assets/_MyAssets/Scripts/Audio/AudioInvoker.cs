
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioInvoker: MonoBehaviour
{
    [SerializeField] private SOAudio config;

    [Button]
    public void PlayAudio()
    {
        config.Play();
    }
}