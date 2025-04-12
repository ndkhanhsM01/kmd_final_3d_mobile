
using UnityEngine;
using UnityEngine.AI;

namespace Monster.Janitor
{
    public class ChaseState : BaseState<ContextParam>
    {
        private Transform target;
        private float timer;
        private NavMeshAgent agent => contextParam.Agent;
        public ChaseState(Janitor context, ContextParam stats) 
            : base(context, stats)
        {
        }

        public override void Enter()
        {
            if (!GameplayController.MC)
            {
                Debug.Log("MC not found!");
                return;
            }

            contextParam.VisionAttacker.IsActive = true;
            agent.speed = contextParam.ChaseSpeed;
            target = GameplayController.MC.Body;
            contextParam.Animator.SetBool(ParamAnimJanitor.IsRunning, true);
        }

        public override void Exit()
        {
            contextParam.VisionAttacker.IsActive = false;
            contextParam.Animator.SetBool(ParamAnimJanitor.IsRunning, false);
        }

        public override void Stay()
        {
            agent.SetDestination(target.position);

            timer += Time.deltaTime;
            bool canGiveUp = timer > contextParam.ChasingDurationMin;
            if(canGiveUp)
                CheckMcOutOfBoundary();
            
            CheckMcInAttackRange();
        }

        private void CheckMcOutOfBoundary()
        {
            if (contextParam.WorkArea.CheckInside(target.position))
                return;

            DebugUtil.Log("MC out of work area -> give up");
            context.SwitchToState<PatrolState>();
        }

        private void CheckMcInAttackRange()
        {
            if (!contextParam.VisionAttacker.CheckInside(target.position))
                return;

            context.SwitchToState<AttackState>();
        }
    }
}