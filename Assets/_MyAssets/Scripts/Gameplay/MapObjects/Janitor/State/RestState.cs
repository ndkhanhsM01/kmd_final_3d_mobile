using UnityEngine;
using Monster;

namespace Monster.Janitor
{
    public class RestState : BaseState<ContextParam>
    {
        private float timer = 0f;
        public RestState(Janitor context, ContextParam stats)
            : base(context, stats)
        {
        }

        public override void Enter()
        {
            timer = 0f;
            contextParam.Animator.SetBool(ParamAnimJanitor.IsWalking, false);
            contextParam.Animator.SetBool(ParamAnimJanitor.IsRunning, false);
        }

        public override void Exit()
        {

        }

        public override void Stay()
        {
            timer += Time.deltaTime;
            if (timer > contextParam.RestDuration)
                context.SwitchToState<PatrolState>();
        }
    }
}