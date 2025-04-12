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
            param.Animator.SetBool(ParamAnimJanitor.IsWalking, false);
            param.Animator.SetBool(ParamAnimJanitor.IsRunning, false);
        }

        public override void Exit()
        {

        }

        public override void Stay()
        {
            timer += Time.deltaTime;
            if (timer > param.RestDuration)
                machine.SwitchToState<PatrolState>();
        }
    }
}