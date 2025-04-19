
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Monster.Bomb
{
    public class MoveState : BaseState<ContextParam>
    {
        private Vector3 targetPoint;
        private Transform body => machine.Body;
        private CancellationTokenSource tokenSource;
        public MoveState(MonsterStateMachine<ContextParam> context, ContextParam contextParam) 
            : base(context, contextParam)
        {
            tokenSource = new();
        }

        public override void Enter()
        {
            targetPoint = param.GetCurrentPoint();

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

                await TaskMoveForward();

                machine.SwitchToState<IdleState>();
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
        private async UniTask TaskMoveForward()
        {
            float remain = 100f;
            float speed = param.moveSpeed;

            param.animator.SetBool(AnimParam.IsRunning, true);
            while (remain > speed * Time.deltaTime)
            {
                body.position = Vector3.MoveTowards(body.position, targetPoint, speed * Time.deltaTime);
                remain = Vector3.Distance(body.position, targetPoint);
                await UniTask.Yield(cancellationToken: tokenSource.Token);
            }
            body.position = targetPoint;
            param.ToNextPoint();
        }
        private async UniTask TaskLookAtTarget()
        {
            Vector3 toward = (targetPoint - body.position).normalized;
            float angle = Mathf.Atan2(toward.x, toward.z) * Mathf.Rad2Deg;
            Quaternion lookAt = Quaternion.Euler(0f, angle, 0f);

            float turnDuration = 1f;
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