
using UnityEngine;
using UnityEngine.AI;

namespace Monster.Janitor
{
    public class ChaseState : BaseState<ContextParam>
    {
        private Transform target;
        private float timer;
        private NavMeshAgent agent => param.Agent;
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

            param.VisionAttacker.IsActive = true;
            agent.speed = param.ChaseSpeed;
            target = GameplayController.MC.Body;
            param.Animator.SetBool(ParamAnimJanitor.IsRunning, true);
        }

        public override void Exit()
        {
            param.VisionAttacker.IsActive = false;
            param.Animator.SetBool(ParamAnimJanitor.IsRunning, false);
        }

        public override void Stay()
        {
            agent.SetDestination(target.position);

            timer += Time.deltaTime;
            bool canGiveUp = timer > param.ChasingDurationMin;
            if(canGiveUp)
                CheckMcOutOfBoundary();
            
            CheckMcInAttackRange();
        }

        private void CheckMcOutOfBoundary()
        {
            if (param.WorkArea.CheckInside(target.position))
                return;

            DebugUtil.Log("MC out of work area -> give up");
            machine.SwitchToState<PatrolState>();
        }

        private void CheckMcInAttackRange()
        {
            if (!param.VisionAttacker.CheckInside(target.position))
                return;

            machine.SwitchToState<AttackState>();
        }
    }
}