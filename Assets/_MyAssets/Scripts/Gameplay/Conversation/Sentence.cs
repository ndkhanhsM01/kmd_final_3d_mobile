using System.Text;
using UnityEngine;

[System.Serializable]
public class Sentence
{
    public SOSpeaker Owner;
    [TextArea] public string Content;

    public StringBuilder GetSBContent()
    {
        return new StringBuilder(Content);
    }
}