
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Monster.Rusher
{
    public class AttackState : BaseState<ContextParam>
    {
        private Vector3 targetPoint;
        private Transform body => context.Body;
        private CancellationTokenSource tokenSource;
        public AttackState(MonsterStateMachine<ContextParam> context, ContextParam contextParam)
            : base(context, contextParam)
        {
            tokenSource = new();
        }

        public override void Enter()
        {
            targetPoint = GameplayController.MC.Body.position;
            HandleState();
        }

        public override void Exit()
        {
            if (tokenSource != null)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = null;
            }
        }

        public override void Stay()
        {
        }

        private async void HandleState()
        {
            try
            {
                await TaskLookAtTarget();

                await TaskRush();

                context.SwitchToState<IdleState>();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("HandleState was canceled.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error: {ex}");
            }
        }
        private async UniTask TaskRush()
        {
            float remain = 100f;
            float speed = contextParam.rushSpeed;

            while (remain > speed * Time.deltaTime)
            {
                body.position = Vector3.MoveTowards(body.position, targetPoint, speed * Time.deltaTime);
                remain = Vector3.Distance(body.position, targetPoint);
                await UniTask.Yield(cancellationToken: tokenSource.Token);
            }
        }
        private async UniTask TaskLookAtTarget()
        {
            Vector3 toward = (targetPoint - body.position).normalized;
            float angle = Mathf.Atan2(toward.x, toward.z) * Mathf.Rad2Deg;
            Quaternion lookAt = Quaternion.Euler(0f, angle, 0f);

            float turnDuration = 0.15f;
            float elapsedTime = 0f;
            while (elapsedTime < turnDuration)
            {
                elapsedTime += Time.deltaTime;
                body.localRotation = Quaternion.Lerp(body.localRotation, lookAt, elapsedTime / turnDuration);
                await UniTask.Yield(cancellationToken: tokenSource.Token);
            }
            body.localRotation = lookAt;
        }
    }
}