

using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MLib
{
    public class MPopup: MonoBehaviour
    {
        [SerializeField] private Transform main;
        [SerializeField] private Button btnClose;
        [SerializeField] private GameObject cover;

        [Header("Show")]
        [SerializeField] private float showDuration = 0.25f;
        [SerializeField] private Vector3 showScale = Vector3.one;
        [SerializeField] private Ease easeShow = Ease.OutBack;
        [SerializeField] private UnityEvent onShowDone;

        [Header("Hide")]
        [SerializeField] private float hideDuration = 0.15f;
        [SerializeField] private Vector3 hideScale = Vector3.zero;
        [SerializeField] private Ease easeHide = Ease.Linear;
        [SerializeField] private UnityEvent onHideDone;

        private Tween tween;

        protected virtual void OnEnable()
        {
            if(btnClose)
                btnClose.AddListener(Hide);
        }
        protected virtual void OnDisable()
        {
            if (btnClose)
                btnClose.RemoveListener(Hide);
        }
        public virtual void Show()
        {
            if (tween != null)
                tween.Kill();

            if (cover)
                cover.SetActive(true);
            main.SetActive(true);

            main.localScale = hideScale;
            tween = main.DOScale(showScale, showDuration).SetEase(easeShow);

            tween.OnComplete(() =>
            {
                onShowDone?.Invoke();
            });
        }

        public virtual void Hide()
        {
            if (tween != null)
                tween.Kill();

            tween = main.DOScale(hideScale, hideDuration).SetEase(easeHide);

            tween.OnComplete(() =>
            {
                if (cover)
                    cover.SetActive(false);
                main.SetActive(false);
                onHideDone?.Invoke();
            });
        }


#if UNITY_EDITOR
        [MButton]
        protected void Show_Editor()
        {
            if (cover)
                cover.SetActive(true);
            main.SetActive(true);
        }

        [MButton]
        protected void Hide_Editor()
        {
            if (cover)
                cover.SetActive(false);
            main.SetActive(false);
        }
#endif
    }
}