using UnityEngine;
using MLib;
using System;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PanelRevive: MPanel
{
    [SerializeField] private SOIntVariable sharedCoinRevive;
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private SOVoidEventChannel reviveChannel;
    [SerializeField] private float delayReject = 5f;
    [SerializeField] private Button btnRevive;
    [SerializeField] private Button btnCancel;
    [SerializeField] private TMP_Text tmpCountdown;
    [SerializeField] private TMP_Text tmpCoinRequire;

    private Coroutine crDelayReject;
    private void OnEnable()
    {
        btnRevive.AddListener(OnClick_Revive);
        btnCancel.AddListener(OnClick_No);
    }
    private void OnDisable()
    {
        btnRevive.RemoveListener(OnClick_Revive);
        btnCancel.RemoveListener(OnClick_No);
    }
    public override void Show(Action onFinish)
    {
        base.Show(onFinish);
        tmpCoinRequire.text = sharedCoinRevive.Value.ToString();
        crDelayReject = StartCoroutine(IE_DelayReject());
    }
    private void OnClick_Revive()
    {
        sharedCoin.Value -= sharedCoinRevive.Value;
        reviveChannel.Raise();
        StopCountdown();

        Hide();
        MUIManager.Instance.ShowPanel<PanelGameplay>();
    }
    private void OnClick_No()
    {
        Reject();
        StopCountdown();
    }
    private IEnumerator IE_DelayReject()
    {
        var waiter = new WaitForSeconds(1f);
        float timer = delayReject;
        while (timer > 0f)
        {
            timer -= 1f;
            tmpCountdown.text = Mathf.FloorToInt(timer).ToString();
            yield return waiter;
        }

        Reject();
    }
    private void StopCountdown()
    {
        if(crDelayReject != null)
            StopCoroutine(crDelayReject);
    }
    private void Reject()
    {
        MUIManager.Instance.ShowPanel<PanelGameLose>();
        Hide();
    }
}