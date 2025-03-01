using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MLib
{
    public abstract class MPanel : MonoBehaviour
    {
        [System.Serializable]
        public class Setting
        {
            public float introDuration = 0f;
            public float outroDuration = 0.5f;
            public UnityEvent OnShow;
            public UnityEvent OnHide;
        }
        [SerializeField] private Setting setting = new();
        [SerializeField] protected Canvas canvas;

        protected void Reset()
        {
            canvas = GetComponent<Canvas>();            
        }
        public void Show()
        {
            Show(null);
        }

        public void Hide()
        {
            Hide(null);
        }

        public virtual void Show(Action onFinish)
        {
            canvas.enabled = true;
            setting.OnShow?.Invoke();
            this.DelayRealtimeCall(setting.introDuration, () =>
            {
                onFinish?.Invoke();
            });
        }
        public virtual void Hide(Action onFinish)
        {
            setting.OnHide?.Invoke();
            this.DelayRealtimeCall(setting.outroDuration, () =>
            {
                canvas.enabled = false;
                onFinish?.Invoke();
            });
        }

#if UNITY_EDITOR
        [MButton]
        protected void Show_Editor()
        {
            Show(null);
        }

        [MButton]
        protected void Hide_Editor()
        {
            Hide(null);
        }
#endif
    }
}
