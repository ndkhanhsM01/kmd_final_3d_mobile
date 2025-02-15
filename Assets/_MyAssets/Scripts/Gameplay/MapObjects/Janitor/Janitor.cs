using MLib;
using Monster;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Monster.Janitor
{
    public class ParamAnimJanitor
    {
        public static int Attack = Animator.StringToHash("attack");
        public static int IsWalking = Animator.StringToHash("isWalking");
        public static int IsRunning = Animator.StringToHash("isRunning");
    }
    public class Janitor : MonsterStateMachine<ContextParam>
    {
        private void OnEnable()
        {
            contextParam.McDetector.Register_McEnter(OnDetectMC);
        }
        private void OnDisable()
        {
            contextParam.McDetector.ClearAllListeners();
        }

        protected override void Start()
        {
            base.Start();
            SwitchToState<PatrolState>();
            contextParam.McDetector.StartScan();
        }

        private void OnDetectMC()
        {
            SwitchToState<ChaseState>();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            contextParam.WorkArea.DrawEditor(Color.red);
        }
#endif
    }

    [System.Serializable]
    public class ContextParam : MonsterParam
    {
        public NavMeshAgent Agent;
        public MCDetector McDetector;
        public VisionAttacker VisionAttacker;
        public Animator Animator;
        public float ForceAttack;
        public SOVector3EventChannel pushMcChannel;

        public float MoveSpeed;
        public float ChaseSpeed;
        public float TurnSpeed;
/*        public float AttackAngle;
        public float AttackRange;*/
        public float ChasingDurationMin;
        public float RestDuration;
        public SquareBoundary WorkArea;
    }
}
