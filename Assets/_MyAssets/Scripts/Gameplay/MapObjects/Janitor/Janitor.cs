using MLib;
using Monster;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Monster.Janitor
{
    public class Janitor : MonsterStateMachine<Stats, ComponentsContainer>
    {
        private void OnEnable()
        {
            components.McDetector.Register_McEnter(OnDetectMC);
        }
        private void OnDisable()
        {
            components.McDetector.ClearAllListeners();
        }

        protected override void Start()
        {
            base.Start();
            SwitchToState<PatrolState>();
            components.McDetector.StartScan();
        }

        private void OnDetectMC()
        {
            SwitchToState<ChaseState>();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            stats.WorkArea.DrawEditor(Color.red);
        }
#endif
    }

    [System.Serializable]
    public class Stats : MonsterStats
    {
        public float MoveSpeed;
        public float ChaseSpeed;
        public float TurnSpeed;
        public float AttackAngle;
        public float AttackRange;
        public float ChasingDurationMin;
        public float RestDuration;
        public SquareBoundary WorkArea;
    }

    [System.Serializable]
    public class ComponentsContainer : MonsterComponentsContainer
    {
        public NavMeshAgent Agent;
        public MCDetector McDetector;
        public VisionAttacker VisionAttacker;
    }
}
