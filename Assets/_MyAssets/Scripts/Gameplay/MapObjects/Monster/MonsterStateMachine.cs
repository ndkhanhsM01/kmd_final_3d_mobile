using MLib;
using System;
using UnityEngine;

namespace Monster
{
    public abstract class MonsterStateMachine<S, C>: MonoBehaviour
        where S: MonsterStats 
        where C: MonsterComponentsContainer
    {
        [SerializeField] protected bool showDebug;
        [SerializeField] protected S stats;
        [SerializeField] protected C components;

        protected BaseState<S, C> currentState;

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
            where T : BaseState<S, C>
        {
            if (currentState != null)
            {
                currentState.Exit();
                if (showDebug) DebugUtil.Log($"<color=#FF0000>Exit</color> <{currentState.GetType()}>");
            }

            BaseState<S, C> newState = (T)Activator.CreateInstance(typeof(T), this, stats, components);

            currentState = newState;
            currentState.Enter();
            if (showDebug) DebugUtil.Log($"<color=#00FF00>Enter</color> <{currentState.GetType()}>");
            return newState as T;
        }
    }

    [Serializable]
    public abstract class MonsterStats
    {

    }

    [Serializable]
    public abstract class MonsterComponentsContainer
    {

    }
}