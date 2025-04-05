
using MLib;
using UnityEditor;
using UnityEngine;

public class SOAudio : ScriptableObject
{
    [SerializeField] private bool isLoop;
    [SerializeField] private bool isRandom;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField, Range(0f, 1f)] private float ratioRevive = 0.15f;
    [SerializeField] private AudioClip[] clips;
    public bool IsLoop => isLoop;
    public bool IsRandom => isRandom;
    public float Volume => volume;
    public float RatioRevive => ratioRevive;

    private int id = 0;
    public void Play()
    {
        if (!AudioManager.Instance)
            return;
        if(isLoop)
            AudioManager.Instance.PlayMusic(this);
        else
            AudioManager.Instance.PlaySound(this);
    }
    public int GetKey()
    {
        if (id == 0)
            id = this.GetInstanceID();
        return id;
    }
    public AudioClip GetClip()
    {
        if (IsRandom)
            return clips.GetRandom();
        else
            return clips[0];
    }

#if UNITY_EDITOR
    public void Editor_Init(AudioClip clip)
    {
        clips = new AudioClip[1];
        clips[0] = clip;
    }
#endif
}