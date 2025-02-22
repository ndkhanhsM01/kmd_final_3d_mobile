
using UnityEngine;
using UnityEngine.UI;
using MLib;
using TMPro;
using System.Text;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class PanelConversation: MPanel
{
    [SerializeField] private TMP_Text tmpContent;
    [SerializeField] private Image imgSpeaker;

    [Header("Configure")]
    [SerializeField] private float textInterval = 0.1f;
    [SerializeField] private float sentenceInterval = 0.5f;

    private Conversation current;
    private Queue<Sentence> queueSentences;
    public void SetAvatar(Sprite avatar)
    {
        imgSpeaker.sprite = avatar;
    }
    public void SetText(StringBuilder sb)
    {
        tmpContent.text = sb.ToString();
    }
    public void Play(Conversation conversation)
    {
        current = conversation;
        queueSentences = current.GetQueueSentences();

        Show();
        Task_HandleConversation().Forget();
    }

    private async UniTask Task_HandleConversation()
    {
        current.RaiseStart();

        while (queueSentences.Count > 0)
        {
            var sentence = queueSentences.Dequeue();
            await Task_HandleSentence(sentence);
            await UniTask.WaitForSeconds(sentenceInterval);
        }

        current.RaiseEnd();
        Hide();
    }
    private async UniTask Task_HandleSentence(Sentence sentence)
    {
        StringBuilder sb = new StringBuilder();
        SetAvatar(sentence.Owner.Avatar);
        int index = 0;
        while (index < sentence.Content.Length)
        {
            sb.Append(sentence.Content[index]);
            SetText(sb);
            index++;
            await UniTask.WaitForSeconds(textInterval);
        }
    }
}