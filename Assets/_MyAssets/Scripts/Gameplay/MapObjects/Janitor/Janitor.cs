using MLib;
using Monster;
using Sirenix.OdinInspector;
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
        protected override void Awake()
        {
            base.Awake();
            contextParam.CenterZone = Body.position;
        }
        private void OnEnable()
        {
            SwitchToState<PatrolState>();
            contextParam.McDetector.StartScan();

            contextParam.McDetector.Register_McEnter(OnDetectMC);
        }
        private void OnDisable()
        {
            contextParam.McDetector.ClearAllListeners();
        }

        private void OnDetectMC()
        {
            SwitchToState<ChaseState>();
        }

#if UNITY_EDITOR
        [Button]
        private void SetOriginWorkArea()
        {
            contextParam.WorkArea.horizontal = new RangeFloat() { min = transform.position.x - 5f, max = transform.position.x + 5f };
            contextParam.WorkArea.vertical = new RangeFloat() { min = transform.position.z - 5f, max = transform.position.z + 5f };
            UnityEditor.EditorUtility.SetDirty(this);
        }
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
        [HideInInspector] public Vector3 CenterZone;
    }
}
