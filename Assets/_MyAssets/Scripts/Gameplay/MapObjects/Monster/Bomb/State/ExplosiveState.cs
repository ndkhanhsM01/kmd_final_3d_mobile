
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

namespace Monster.Bomb
{
    public class ExplosiveState : BaseState<ContextParam>
    {
        private Transform body => machine.Body;
        private CancellationTokenSource tokenSource;
        public ExplosiveState(MonsterStateMachine<ContextParam> context, ContextParam contextParam) 
            : base(context, contextParam)
        {
            tokenSource = new();
        }

        public override void Enter()
        {
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
                param.animator.SetBool(AnimParam.IsRunning, true);
                await TaskWarning();

                await TaskExplosion();

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

        private async UniTask TaskWarning()
        {
            param.warning.SetActive(true);
            await UniTask.WaitForSeconds(param.warningDuration, cancellationToken: tokenSource.Token);
            param.warning.SetActive(false);
        }

        private async UniTask TaskExplosion()
        {
            await UniTask.WaitForSeconds(0.15f, cancellationToken: tokenSource.Token);

            KillArround();

            await UniTask.WaitForSeconds(0.25f, cancellationToken: tokenSource.Token);
        }

        private void KillArround()
        {
            param.fxExplosion.Play();
            param.audioExplosion.Play();

            Collider[] colliders = Physics.OverlapSphere(body.position, param.explosionRadius);
            if (colliders == null || colliders.Length <= 0)
            {

            }
            else
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out IReceiveDamage receiver))
                    {
                        receiver.ReceiveDamage(body);
                        receiver.ReceiveForce(Vector3.up * 10f);
                    }
                }
            }
        }
    }
}