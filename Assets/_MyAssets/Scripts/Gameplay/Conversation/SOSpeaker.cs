

using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "Conversation/Speaker")]
public class SOSpeaker: ScriptableObject
{
    [SerializeField] private Sprite avatar;
    [SerializeField] private string nameSpeaker;

    public Sprite Avatar => avatar;
    public string NameSpeaker => nameSpeaker;
}