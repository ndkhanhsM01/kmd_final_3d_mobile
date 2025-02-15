using MLib;
using System;
using UnityEngine;

namespace Monster
{
    public abstract class MonsterStateMachine<S>: MonoBehaviour
        where S: MonsterParam 
    {
        [SerializeField] protected bool showDebug;
        [SerializeField] protected S contextParam;

        protected BaseState<S> currentState;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
            if (currentState != null)
            {
                currentState.Stay();
            }
        }

        public virtual T SwitchToState<T>() 
            where T : BaseState<S>
        {
            if (currentState != null)
            {
                currentState.Exit();
                if (showDebug) DebugUtil.Log($"<color=#FF0000>Exit</color> <{currentState.GetType()}>");
            }

            BaseState<S> newState = (T)Activator.CreateInstance(typeof(T), this, contextParam);

            currentState = newState;
            currentState.Enter();
            if (showDebug) DebugUtil.Log($"<color=#00FF00>Enter</color> <{currentState.GetType()}>");
            return newState as T;
        }
    }

    [Serializable]
    public abstract class MonsterParam
    {

    }
}