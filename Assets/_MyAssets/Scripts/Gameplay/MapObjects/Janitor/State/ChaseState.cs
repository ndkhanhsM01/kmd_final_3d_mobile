
using UnityEngine;
using UnityEngine.AI;

namespace Monster.Janitor
{
    public class ChaseState : BaseState<Stats, ComponentsContainer>
    {
        private Transform target;
        private float timer;
        private NavMeshAgent agent => components.Agent;
        public ChaseState(Janitor context, Stats stats, ComponentsContainer components) 
            : base(context, stats, components)
        {
        }

        public override void Enter()
        {
            if (!GameplayController.Instance.MC)
            {
                Debug.Log("MC not found!");
                return;
            }

            components.VisionAttacker.IsActive = true;
            agent.speed = stats.ChaseSpeed;
            target = GameplayController.Instance.MC.Body;
        }

        public override void Exit()
        {
            components.VisionAttacker.IsActive = false;
        }

        public override void Stay()
        {
            agent.SetDestination(target.position);

            timer += Time.deltaTime;
            bool canGiveUp = timer > stats.ChasingDurationMin;
            if(canGiveUp)
                CheckMcOutOfBoundary();
            
            CheckMcInAttackRange();
        }

        private void CheckMcOutOfBoundary()
        {
            if (stats.WorkArea.CheckInside(target.position))
                return;

            DebugUtil.Log("MC out of work area -> give up");
            context.SwitchToState<PatrolState>();
        }

        private void CheckMcInAttackRange()
        {
            if (!components.VisionAttacker.CheckInside(target.position))
                return;

            context.SwitchToState<AttackState>();
        }
    }
}