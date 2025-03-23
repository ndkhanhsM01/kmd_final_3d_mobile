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
            contextParam.isDeath = true;
            contextParam.Animator.SetTrigger(ParamAnimJanitor.Death);
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
            contextParam.Agent.enabled = false;
            contextParam.Hitbox.enabled = false;
            contextParam.McDetector.StopScan();
        }
    }
}