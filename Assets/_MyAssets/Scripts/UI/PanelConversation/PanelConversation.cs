
using UnityEngine;
using UnityEngine.UI;
using MLib;
using TMPro;
using System.Text;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public class PanelConversation: MPanel
{
    [SerializeField] private TMP_Text tmpContent;
    [SerializeField] private TMP_Text tmpName;
    [SerializeField] private Image imgSpeaker;
    [SerializeField] private Button btnNext;

    [Header("Configure")]
    [SerializeField] private float textInterval = 0.1f;
    [SerializeField] private float sentenceInterval = 0.5f;

    private Conversation current;
    private Queue<Sentence> queueSentences;
    private CancellationTokenSource cancelAnimText;

    private void OnEnable()
    {
        btnNext.AddListener(OnClick_Next);
    }
    private void OnDisable()
    {
        btnNext.RemoveListener(OnClick_Next);
    }
    public void SetAvatar(Sprite avatar)
    {
        imgSpeaker.sprite = avatar;
    }
    public void SetName(string name)
    {
        tmpName.text = name;
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

        current.RaiseStart();
        ContinueConversation();
    }
    private async UniTask Task_HandleSentence(Sentence sentence)
    {
        cancelAnimText = new();

        StringBuilder sb = new StringBuilder();
        SetAvatar(sentence.Owner.Avatar);
        SetName(sentence.Owner.NameSpeaker);
        int index = 0;
        int totalChar = sentence.Content.Length;
        while (index < totalChar)
        {
            sb.Append(sentence.Content[index]);
            SetText(sb);
            index++;

            btnNext.SetActive((float) index/totalChar > 0.5f);
            await UniTask.WaitForSeconds(textInterval, cancellationToken: cancelAnimText.Token);
        }
    }
    private bool ContinueConversation()
    {
        StopCurrentSentence();
        btnNext.SetActive(false);

        if(queueSentences.Count <= 0)
        {
            current.RaiseEnd();
            return false;
        }

        var sentence = queueSentences.Dequeue();
        Task_HandleSentence(sentence).Forget();
        return true;
    }
    private void StopCurrentSentence()
    {
        if(cancelAnimText != null)
        {
            cancelAnimText.Cancel();
            cancelAnimText.Dispose();
            cancelAnimText = null;
        }
    }

    private void OnClick_Next()
    {
        bool keepPlay = ContinueConversation();

        if (!keepPlay)
        {
            Hide();
        }
    }
}