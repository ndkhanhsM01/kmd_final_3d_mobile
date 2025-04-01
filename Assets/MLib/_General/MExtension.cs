
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MLib
{
    public static class MExtension
    {
        #region MonoBehaviour
        public static void Active(this MonoBehaviour m)
        {
            m.gameObject.SetActive(true);
        }
        public static void Deactive(this MonoBehaviour m)
        {
            m.gameObject.SetActive(false);
        }

        public static void SetActive(this MonoBehaviour m, bool active)
        {
            m.gameObject.SetActive(active);
        }

        public static void SetActive(this Component m, bool active)
        {
            m.gameObject.SetActive(active);
        }

        public static Coroutine DelayCall(this MonoBehaviour m, float delay, Action callback)
        {
            return m.StartCoroutine(IE_DelayCall());

            IEnumerator IE_DelayCall()
            {
                yield return new WaitForSeconds(delay);

                callback?.Invoke();
            }
        }

        public static Coroutine DelayRealtimeCall(this MonoBehaviour m, float delay, Action callback)
        {
            return m.StartCoroutine(IE_DelayCall());

            IEnumerator IE_DelayCall()
            {
                yield return new WaitForSecondsRealtime(delay);

                callback?.Invoke();
            }
        }
        public static List<T> ClonePrefabsInside<T>(this MonoBehaviour m, T prefab, int amount, bool hide = false) 
            where T : MonoBehaviour
        {
            List<T> result = new List<T>();
            Transform parent = m.transform;
            for (int i = 0; i < amount; i++)
            {
                T clone = GameObject.Instantiate(prefab, parent);
                clone.gameObject.SetActive(!hide);
                result.Add(clone);
            }
            return result;
        }

        public static Tween DoAnimPickup(this Transform transform, float up, TweenCallback onComplete = null)
        {
            transform.DOKill();
            Camera camera = Camera.main;
            Vector3 lookDir = (camera.transform.position - transform.position).normalized;
            float beginScale = transform.localScale.x;
            Quaternion rotationTarget = Quaternion.LookRotation(lookDir, -Vector3.right) * Quaternion.Euler(Vector3.right * 90f);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(transform.position.y + up, 0.75f))
                    .Join(transform.DORotateQuaternion(rotationTarget, 0.25f))
                    .Append(transform.DOScale(beginScale * 1.1f, 0.07f))
                    .Append(transform.DOScale(0f, 0.15f));

            sequence.onComplete += onComplete;
            return sequence;
        }
        public static List<GameObject> GetChildrenWithNameContains(this Transform parent, string keyword)
        {
            List<GameObject> result = new List<GameObject>();
            FindChildrenRecursive(parent, keyword, result);
            return result;
        }

        private static void FindChildrenRecursive(Transform parent, string keyword, List<GameObject> list)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains(keyword))
                {
                    list.Add(child.gameObject);
                }
                FindChildrenRecursive(child, keyword, list);
            }
        }
        #endregion

        #region List
        public static bool IsOutOfRange<T>(this List<T> list, int index)
        {
            return index >= list.Count || index < 0;
        }
        public static bool IsOutOfRange<T>(this T[] arr, int index)
        {
            return index >= arr.Length || index < 0;
        }
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                return default(T);

            return list[Random.Range(0, list.Count)];
        }
        public static T GetRandom<T>(this T[] arr)
        {
            if (arr == null || arr.Length == 0)
                return default(T);

            return arr[Random.Range(0, arr.Length)];
        }
        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }

        #endregion

        #region UI
        public static void AddListener(this Button button, UnityAction callback)
        {
            button.onClick.AddListener(callback);
        }
        public static void RemoveListener(this Button button, UnityAction callback)
        {
            button.onClick.RemoveListener(callback);
        }
        public static void SetAlpha(this Image img, float alpha)
        {
            var color = img.color;
            color.a = alpha;
            img.color = color;
        }
        #endregion
    }
}