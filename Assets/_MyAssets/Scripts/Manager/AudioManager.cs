

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
    private const string nameMusicGroup = "Music";
    private const string nameSFXGroup = "SFX";

    private HashSet<int> soundPlaying;
    private HashSet<int> musicPlaying;
    private LocalData localData => DataManager.LocalData;
    protected override void Awake()
    {
        base.Awake();
        soundPlaying = new();
        musicPlaying = new();
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
    public void PlayMusic(SOAudio config)
    {
        srcMusic.clip = config.GetClip();
        srcMusic.volume = config.Volume;
        srcMusic.Play();
    }
    public void StopMusic()
    {
        srcMusic.Stop();
    }

    public void PlaySound(SOAudio config, float durationRequire = -1f)
    {
        if (soundPlaying.Contains(config.GetKey()))
            return;

        var clip = config.GetClip();
        srcSound.volume = config.Volume;
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
        DelayAllowSound(config.GetKey(), clip.length * config.RatioRevive).Forget();
    }

    private async UniTask DelayAllowSound(int key, float delay)
    {
        soundPlaying.Add(key);
        await UniTask.WaitForSeconds(delay);
        soundPlaying.Remove(key);
    }
    private async UniTask DelayAllowMusic(int key, float delay)
    {
        musicPlaying.Add(key);
        await UniTask.WaitForSeconds(delay);
        musicPlaying.Remove(key);
    }
}