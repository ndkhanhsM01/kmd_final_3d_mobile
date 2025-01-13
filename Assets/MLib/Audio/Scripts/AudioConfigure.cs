

using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    [CreateAssetMenu(fileName = "MAudioConfigure", menuName = "MLib/Audio Configure")]
    public class AudioConfigure : ScriptableObject
    {
        [SerializeField] private MAudio[] AllAudio;

        public Dictionary<MSoundType, MAudio> GetDictAudio()
        {
            Dictionary<MSoundType, MAudio> result = new Dictionary<MSoundType, MAudio>();

            foreach (var audio in AllAudio)
            {
                result.Add(audio.type, audio);
            }

            return result;
        }
    }

    [System.Serializable]
    public struct MAudio
    {
        public MSoundType type;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
    }

    public enum MSoundType
    {
        // example
        Background,
        ClickButton,
        Win
    }
}