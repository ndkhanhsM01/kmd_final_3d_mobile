
using System;

namespace Monster
{
    public abstract class BaseState<S>
        where S: MonsterParam
    {
        protected MonsterStateMachine<S> machine;
        protected S param;
        public BaseState(MonsterStateMachine<S> context, S contextParam)
        {
            this.machine = context;
            this.param = contextParam;
        }
        public abstract void Enter();
        public abstract void Stay();
        public abstract void Exit();
    }
}