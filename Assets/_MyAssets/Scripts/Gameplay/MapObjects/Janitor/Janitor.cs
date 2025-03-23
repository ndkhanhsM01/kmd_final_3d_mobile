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
        public static int Death = Animator.StringToHash("death");
    }
    public class Janitor : MonsterStateMachine<ContextParam>, IReceiveDamage
    {
        private Vector3 initPosition;
        private bool isInit = false;
        protected override void Awake()
        {
            base.Awake();
            contextParam.CenterZone = Body.position;
            initPosition = Body.position;

            isInit = true;
        }
        private void OnEnable()
        {
            if(!contextParam.isDeath)
            {
                SwitchToState<PatrolState>();
                contextParam.McDetector.StartScan();

                contextParam.McDetector.Register_McEnter(OnDetectMC);

                if(isInit)
                {
                    Body.position = initPosition;
                }
            }
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

        public bool ReceiveDamage(Transform source)
        {
            SwitchToState<DeathState>();
            return true;
        }

        public void ReceiveForce(Vector3 force)
        {
            contextParam.Ragdoll.AddForce(force);
        }
#endif
    }

    [System.Serializable]
    public class ContextParam : MonsterParam
    {
        [ReadOnly] public bool isDeath;
        public NavMeshAgent Agent;
        public MCDetector McDetector;
        public VisionAttacker VisionAttacker;
        public Animator Animator;
        public CharacterRagdoll Ragdoll;
        public Collider Hitbox;
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
