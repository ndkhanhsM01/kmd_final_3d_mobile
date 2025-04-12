
using UnityEngine;

namespace Monster.Bomb
{
    public class IdleState : BaseState<ContextParam>
    {
        private float timer;
        public IdleState(MonsterStateMachine<ContextParam> context, ContextParam contextParam)
            : base(context, contextParam)
        {
        }

        public override void Enter()
        {
            timer = contextParam.idleDuration.GetRandomValue();
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                context.SwitchToState<RushState>();
            }
        }
    }
}