

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
        [SerializeField] private AnimationScalePopup animPopup;

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

            animPopup.DoShow();
        }

        public virtual void Hide()
        {
            if (tween != null)
                tween.Kill();

            animPopup.DoHide();
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