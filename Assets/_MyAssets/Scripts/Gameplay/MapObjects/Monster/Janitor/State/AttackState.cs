using UnityEngine;

namespace Monster.Janitor
{
    public class AttackState : BaseState<ContextParam>
    {
        private float delayLose = 1f;
        private float timer;
        public AttackState(MonsterStateMachine<ContextParam> context, ContextParam contextParam) 
            : base(context, contextParam)
        {
        }

        public override void Enter()
        {
            KillMC();

            timer = delayLose;
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
            if (timer < 0f)
                return;

            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                GameplayController.Instance.LoseLevel();
            }
        }
        private void KillMC()
        {
            StopMove();
            GameplayController.MC.Action.SetMotion(false);

            Transform target = GameplayController.MC.Body;
            Vector3 direction = (target.position - machine.transform.position).normalized;

            param.Animator.SetTrigger(ParamAnimJanitor.Attack);
            param.pushMcChannel.Raise(direction * param.ForceAttack);    
        }
        private void StopMove()
        {
            param.Agent.SetDestination(machine.transform.position);
        }
    }
}