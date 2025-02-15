using UnityEngine;
using Monster;
using UnityEngine.AI;
using System.Collections;

namespace Monster.Janitor
{
    public class PatrolState : BaseState<ContextParam>
    {
        private NavMeshAgent agent => contextParam.Agent;
        private Vector3 curTarget;
        private bool isDoneBeginRotate;
        private byte countFrameCaculate;
        public PatrolState(Janitor context, ContextParam stats)
            : base(context, stats)
        {
        }

        public override void Enter()
        {
            curTarget = contextParam.WorkArea.GetRandomPosition();
            agent.speed = contextParam.MoveSpeed;

            context.StartCoroutine(IE_BeginRotate());
            contextParam.Animator.SetBool(ParamAnimJanitor.IsWalking, true);
        }

        public override void Exit()
        {
            contextParam.Animator.SetBool(ParamAnimJanitor.IsWalking, false);
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
            if (countFrameCaculate < 1)
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
                body.rotation = Quaternion.Lerp(body.rotation, lookRotation, contextParam.TurnSpeed * Time.deltaTime);
                yield return null;
            }

            isDoneBeginRotate = true;
        }
    }
}