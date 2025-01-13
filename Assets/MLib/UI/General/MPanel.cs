using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public abstract class MPanel : MonoBehaviour
    {
        [System.Serializable]
        public class Setting
        {
            public float introDuration = 0f;
            public float outroDuration = 0.5f;
        }
        [SerializeField] private Setting setting = new();

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
            this.DelayRealtimeCall(setting.introDuration, onFinish);
        }
        public virtual void Hide(Action onFinish)
        {
            this.DelayRealtimeCall(setting.outroDuration, onFinish);
        }
    }
}
