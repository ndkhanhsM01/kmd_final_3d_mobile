
using System;

namespace Monster
{
    public abstract class BaseState<S>
        where S: MonsterParam
    {
        protected MonsterStateMachine<S> context;
        protected S contextParam;
        public BaseState(MonsterStateMachine<S> context, S contextParam)
        {
            this.context = context;
            this.contextParam = contextParam;
        }
        public abstract void Enter();
        public abstract void Stay();
        public abstract void Exit();
    }
}