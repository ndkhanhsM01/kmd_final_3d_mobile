

using Cysharp.Threading.Tasks;
using MLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MSingleton<AudioManager>
{
    [SerializeField] private AudioSource srcSound;
    [SerializeField] private AudioSource srcMusic;
    [SerializeField] private SOFloatEventChannel soundChannel;
    [SerializeField] private SOFloatEventChannel musicChannel;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioElement[] audioElements;

    private Dictionary<AudioType, AudioElement> dictAudios;
    private const string nameMusicGroup = "Music";
    private const string nameSFXGroup = "SFX";

    private HashSet<AudioType> soundPlaying;
    private HashSet<AudioType> musicPlaying;
    private LocalData localData => DataManager.LocalData;
    protected override void Awake()
    {
        base.Awake();
        dictAudios = new();
        soundPlaying = new();
        musicPlaying = new();
        foreach (var element in audioElements)
        {
            dictAudios.Add(element.type, element);
        }
    }
    private void OnEnable()
    {
        soundChannel.Register(OnSetSound);
        musicChannel.Register(OnSetMusic);
    }
    private void OnDisable()
    {
        soundChannel.Unregister(OnSetSound);
        musicChannel.Unregister(OnSetMusic);
    }
    public void Reload()
    {
        soundChannel.Raise(localData.VolumeSound);
        musicChannel.Raise(localData.VolumeMusic);
    }
    private void OnSetSound(float value)
    {
        float percent = value;

        float newValue = -80f + (percent * 80f);
        localData.VolumeSound = value;
        mixer.SetFloat(nameSFXGroup, newValue);
    }
    private void OnSetMusic(float value)
    {
        float percent = value;

        float newValue = -80f + (percent * 80f);

        localData.VolumeMusic = value;
        mixer.SetFloat(nameMusicGroup, newValue);
    }
    public void PlayMusic(AudioType type)
    {
        if (!dictAudios.TryGetValue(type, out AudioElement element))
        {
            Debug.LogWarning($"Not found {name} in dictionary!!");
            return;
        }

        srcMusic.clip = element.GetClip();
        srcMusic.volume = element.volume;
        srcMusic.Play();
    }

    public void PlaySound(AudioType type, float durationRequire = -1f)
    {
        if (!dictAudios.TryGetValue(type, out AudioElement element))
        {
            Debug.LogWarning($"Not found {name} in dictionary!!");
            return;
        }

        var clip = element.GetClip();
        if (soundPlaying.Contains(type))
            return;
        srcSound.loop = element.isLoop;
        srcSound.volume = element.volume;
        if (durationRequire > 0f)
        {
            float clipLength = clip.length;
            float pitch = clipLength / durationRequire;
            srcSound.pitch = pitch;
        }
        else
        {
            srcSound.pitch = 1f;
        }

        srcSound.PlayOneShot(clip);
        DelayAllowSound(type, clip.length * element.reviveRate).Forget();
    }

    private async UniTask DelayAllowSound(AudioType type, float delay)
    {
        soundPlaying.Add(type);
        await UniTask.WaitForSeconds(delay);
        soundPlaying.Remove(type);
    }
    private async void DelayAllowMusic(AudioType type, float delay)
    {
        await UniTask.WaitForSeconds(delay);
        musicPlaying.Remove(type);
    }

    [System.Serializable]
    public class AudioElement
    {
        public AudioType type;
        public bool isRandom;
        public bool isLoop;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0f, 1f)] public float reviveRate = 0.3f;
        public AudioClip[] clips;

        public AudioClip GetClip()
        {
            if (isRandom)
                return clips.GetRandom();
            else
                return clips[0];
        }
    }
}



public enum AudioType
{
    None,
    Click
}