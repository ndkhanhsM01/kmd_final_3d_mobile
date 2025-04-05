
using MLib;
using UnityEditor;
using UnityEngine;

public class SOAudio : ScriptableObject
{
    public enum PlayType
    {
        Simple,
        Random,
        Sequence
    }

    [SerializeField] private PlayType type;
    [SerializeField] private bool isLoop;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField, Range(0f, 1f)] private float ratioRevive = 0.15f;
    [SerializeField] private AudioClip[] clips;
    public bool IsLoop => isLoop;
    public float Volume => volume;
    public float RatioRevive => ratioRevive;

    private int id = 0;
    private int indexSequence = 0;

    public void Play()
    {
        Play(-1f);
    }
    public void Play(float durationRequire)
    {
        if (!AudioManager.Instance)
            return;
        if(isLoop)
            AudioManager.Instance.PlayMusic(this);
        else
            AudioManager.Instance.PlaySound(this, durationRequire);
    }
    public void Stop()
    {
        if (!AudioManager.Instance)
            return;
        if (isLoop)
            AudioManager.Instance.StopMusic();
        else
            { /*AudioManager.Instance.PlaySound(this, durationRequire); */}
    }
    public int GetKey()
    {
        if (id == 0)
            id = this.GetInstanceID();
        return id;
    }
    public AudioClip GetClip()
    {
        switch (type)
        {
            case PlayType.Simple:
                return clips[0];
            case PlayType.Random:
                return clips.GetRandom();
            case PlayType.Sequence:
                if (clips.IsOutOfRange(indexSequence))
                    indexSequence = 0;
                var result = clips[indexSequence];
                indexSequence = (indexSequence + 1) % clips.Length;
                return result;
            default:
                return null;
        }
    }

#if UNITY_EDITOR
    public void Editor_Init(AudioClip clip)
    {
        clips = new AudioClip[1];
        clips[0] = clip;
    }
#endif
}