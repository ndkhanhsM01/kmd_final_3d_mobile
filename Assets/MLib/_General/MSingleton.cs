using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public class MSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool destroyOnLoad = true;
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(Instance);
                Debug.LogWarning($"Already exist other instance of <{typeof(T)}>, so destroyed it!");
            }

            if (!destroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
