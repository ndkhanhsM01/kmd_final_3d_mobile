

using DG.Tweening;
using MLib;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition: MonoBehaviour
{
    [SerializeField] private float defaultDuration = 0.75f;
    [SerializeField] private Image imgFade;

    public bool IsDoneIn { get; private set; }
    public bool IsDoneOut { get; private set; }

    private Tween tFade;
    public void DoIn(float duration = -1f)
    {
        KillFade();
        duration = duration > 0f ? duration : defaultDuration;

        imgFade.SetActive(true);
        IsDoneIn = false;

        imgFade.SetAlpha(0f);
        tFade = imgFade.DOFade(1f, duration);
        tFade.OnComplete(() =>
        {
            IsDoneIn = true;
        });
    }
    public void DoOut(float duration = -1f)
    {
        KillFade();
        duration = duration > 0f ? duration : defaultDuration;

        imgFade.SetActive(true);
        IsDoneOut = false;

        tFade = imgFade.DOFade(0f, duration);
        tFade.OnComplete(() =>
        {
            IsDoneOut = true;
            imgFade.SetActive(false);
        });
    }
    private void KillFade()
    {
        if (tFade != null)
            tFade.Kill();
    }
}