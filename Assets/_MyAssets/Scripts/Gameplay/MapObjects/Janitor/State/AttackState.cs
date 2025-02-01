using UnityEngine;

namespace Monster.Janitor
{
    public class AttackState : BaseState<Stats, ComponentsContainer>
    {
        public AttackState(MonsterStateMachine<Stats, ComponentsContainer> context, Stats stats, ComponentsContainer components) : base(context, stats, components)
        {
        }

        public override void Enter()
        {
            GameplayController.Instance.MC.Action.SetMotion(false);
            GameplayController.Instance.LoseLevel();
            StopMove();
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
        }
        private void StopMove()
        {
            components.Agent.SetDestination(context.transform.position);
        }
    }
}