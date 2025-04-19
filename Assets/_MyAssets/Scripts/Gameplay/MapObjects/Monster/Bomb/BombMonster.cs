using MLib;
using UnityEngine;

namespace Monster.Bomb
{
    public class AnimParam
    {
        public static int IsRunning = Animator.StringToHash("isRunning");
    }
    [System.Serializable]
    public class ContextParam: MonsterParam
    {
        public float warningDuration = 1f;
        public float moveSpeed = 5f;
        public float explosionRadius = 2f;
        public RangeFloat idleDuration;
        public GameObject warning;
        public ParticleSystem fxExplosion;
        public SOAudio audioExplosion;
        public Animator animator;
        [SerializeField] private Transform[] points;

        public Transform[] AllPoints => points;

        private int curPointIndex = 0;
        public Vector3 GetStartPoint()
        {
            return points[0].position;
        }
        public void RestartPoint()
        {
            curPointIndex = 0;
        }
        public void ToNextPoint()
        {
            curPointIndex = (curPointIndex + 1) % points.Length;
        }
        public Vector3 GetCurrentPoint()
        {
            return points[curPointIndex].position;
        }
    }
    public class BombMonster: MonsterStateMachine<ContextParam>
    {
        private void OnEnable()
        {
            Respawn();
        }
        public void Respawn()
        {
            Body.position = contextParam.GetStartPoint();
            contextParam.RestartPoint();
            contextParam.ToNextPoint();
            SwitchToState<IdleState>();
        }
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360f, contextParam.explosionRadius);

            Gizmos.color = Color.yellow;
            MHelper.DrawWayPoints(contextParam.AllPoints);
        }
    }
}
