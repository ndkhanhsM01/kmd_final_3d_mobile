using System;
using System.Collections;
using UnityEngine;

namespace MLib
{
    public class UIMoveCurve : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float swing = 10f;
        [SerializeField, Range(-1f, 1f)] private float direction = 0;
        [SerializeField] private AnimationCurve curve;

        private Coroutine coroutine;

        public void DoMove(Transform target, Action onComplete = null)
        {
            Stop();
            coroutine = StartCoroutine(IE_DoMove(target, onComplete));
        }

        private IEnumerator IE_DoMove(Transform target, Action onComplete = null)
        {
            float time = 0f;
            RectTransform rectTarget = target as RectTransform;
            RectTransform rectTransform = transform as RectTransform;
            Vector3 start = rectTransform.position;
            Vector3 end = rectTarget.position;
            float xOffset = start.x + direction * swing;
            while (time <= duration)
            {
                time += Time.deltaTime;

                float linearT = time / duration;
                float heightT = curve.Evaluate(linearT);
                Vector3 newPosition = Vector3.Lerp(start, end, linearT);

                newPosition.x = Mathf.Lerp(newPosition.x, xOffset, heightT);

                rectTransform.position = newPosition;

                yield return null;
            }
            onComplete?.Invoke();
            coroutine = null;
        }

        [MButton]
        private void Stop()
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }
}