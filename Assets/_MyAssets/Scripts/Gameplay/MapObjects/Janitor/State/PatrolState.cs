using UnityEngine;
using Monster;

namespace Monster.Janitor
{
    public class PatrolState : BaseState<Stats, ComponentsContainer>
    {
        private Vector3 curTarget;
        public PatrolState(Janitor context, Stats stats, ComponentsContainer components)
            : base(context, stats, components)
        {
        }

        public override void Enter()
        {
            curTarget = stats.WorkArea.GetRandomPosition();
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
            components.Agent.SetDestination(curTarget);

            if (components.Agent.remainingDistance <= 0.1f)
            {
                context.SwitchToState<RestState>();
            }
        }
    }
}