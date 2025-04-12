using UnityEngine;

namespace Monster.Janitor
{
    public class DeathState : BaseState<ContextParam>
    {
        public DeathState(MonsterStateMachine<ContextParam> context, ContextParam contextParam)
            : base(context, contextParam)
        {
        }

        public override void Enter()
        {
            StopMove();
            param.isDeath = true;
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
        }
        private void StopMove()
        {
            //contextParam.Agent.SetDestination(context.transform.position);
            param.Agent.enabled = false;
            param.Hitbox.enabled = false;
            param.McDetector.StopScan();
        }
    }
}