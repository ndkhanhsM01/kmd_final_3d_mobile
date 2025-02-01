
using System;

namespace Monster
{
    public abstract class BaseState<S, C>
        where S: MonsterStats
        where C: MonsterComponentsContainer
    {
        protected MonsterStateMachine<S, C> context;
        protected S stats;
        protected C components;
        public BaseState(MonsterStateMachine<S, C> context
                        , S stats
                        , C components)
        {
            this.context = context;
            this.stats = stats;
            this.components = components;
        }
        public abstract void Enter();
        public abstract void Stay();
        public abstract void Exit();
    }
}