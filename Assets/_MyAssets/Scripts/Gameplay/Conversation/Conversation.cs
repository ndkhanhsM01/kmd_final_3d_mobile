using MLib;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private SOVoidEventChannel startChannel;
    [SerializeField] private SOVoidEventChannel endChannel;

    [Header("Content")]
    [SerializeField] private Sentence[] sentences;

    [MButton]
    public void Play()
    {
        var panelDisplay = MUIManager.Instance.GetPanel<PanelConversation>();
        panelDisplay.Play(this);
    }

    public void RaiseStart()
    {
        startChannel.Raise();
    }
    public void RaiseEnd()
    {
        endChannel.Raise();
    }
    public Queue<Sentence> GetQueueSentences()
    {
        Queue<Sentence> queue = new Queue<Sentence>(sentences);
        return queue;
    }
}
