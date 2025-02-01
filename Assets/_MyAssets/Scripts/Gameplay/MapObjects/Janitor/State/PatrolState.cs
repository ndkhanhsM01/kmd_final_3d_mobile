using UnityEngine;
using Monster;
using UnityEngine.AI;
using System.Collections;

namespace Monster.Janitor
{
    public class PatrolState : BaseState<Stats, ComponentsContainer>
    {
        private NavMeshAgent agent => components.Agent;
        private Vector3 curTarget;
        private bool isDoneBeginRotate;
        private byte countFrameCaculate;
        public PatrolState(Janitor context, Stats stats, ComponentsContainer components)
            : base(context, stats, components)
        {
        }

        public override void Enter()
        {
            curTarget = stats.WorkArea.GetRandomPosition();
            agent.speed = stats.MoveSpeed;

            context.StartCoroutine(IE_BeginRotate());
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
            if (!isDoneBeginRotate)
                return;

            agent.SetDestination(curTarget);
            CheckRemainDistance();
            countFrameCaculate++;
        }

        private void CheckRemainDistance()
        {
            if (countFrameCaculate < 3)
                return;

            if (agent.remainingDistance <= 0.1f)
            {
                context.SwitchToState<RestState>();
            }

            // reset count frame
            countFrameCaculate = 0;
        }

        private IEnumerator IE_BeginRotate()
        {
            isDoneBeginRotate = false;
            Transform body = context.transform;

            Vector3 lookTarget = curTarget;
            lookTarget.y = body.position.y;
            Vector3 lookDir = (lookTarget - body.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir, Vector3.up);
            while (Vector3.Angle(lookDir, body.forward) > 10f)
            {
                body.rotation = Quaternion.Lerp(body.rotation, lookRotation, stats.TurnSpeed * Time.deltaTime);
                yield return null;
            }

            isDoneBeginRotate = true;
        }
    }
}