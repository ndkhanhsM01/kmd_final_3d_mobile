using UnityEngine;
using Monster;

namespace Monster.Janitor
{
    public class RestState : BaseState<Stats, ComponentsContainer>
    {
        private float timer = 0f;
        public RestState(Janitor context, Stats stats, ComponentsContainer components)
            : base(context, stats, components)
        {
        }

        public override void Enter()
        {
            timer = 0f;
        }

        public override void Exit()
        {

        }

        public override void Stay()
        {
            timer += Time.deltaTime;
            if (timer > stats.RestDuration)
                context.SwitchToState<PatrolState>();
        }
    }
}