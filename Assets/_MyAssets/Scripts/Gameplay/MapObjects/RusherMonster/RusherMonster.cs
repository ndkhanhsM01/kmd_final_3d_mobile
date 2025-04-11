using UnityEngine;

namespace Monster.Rusher
{
    public class ParamAnimRusher
    {
    }
    [System.Serializable]
    public class ContextParam: MonsterParam
    {
        public RangeFloat idleDuration;
        public float delayRush = 1f;
        public float rushSpeed;
        public Animator animator;
        public MCDetector mcDetector;

        [SerializeField] private Transform[] points;

        public Transform[] AllPoints => points;

        private int curPointIndex = 0;
        public Vector3 GetStartPoint()
        {
            return points[0].position;
        }
        public Vector3 GetNextPoint()
        {
            curPointIndex = (curPointIndex + 1) % points.Length;
            return points[curPointIndex].position;
        }
    }
    public class RusherMonster : MonsterStateMachine<ContextParam>, IReceiveDamage
    {
        private void OnEnable()
        {
            contextParam.mcDetector.Register_McEnter(OnDetectMC);
            Respawn();
        }
        private void OnDisable()
        {
            contextParam.mcDetector.Unregister_McEnter(OnDetectMC);
        }
        public void Respawn()
        {
            Body.position = contextParam.GetStartPoint();
            SwitchToState<IdleState>();
        }

        public bool ReceiveDamage(Transform source)
        {
            return false;
        }

        public void ReceiveForce(Vector3 force)
        {

        }
        private void OnDetectMC()
        {
            SwitchToState<AttackState>();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var points = contextParam.AllPoints;
            if (points != null && points.Length < 2) return;
            Gizmos.color = Color.yellow;
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 point1 = points[i].position;
                Vector3 point2 = points[(i + 1) % points.Length].position;
                Gizmos.DrawWireSphere(point1, 0.2f);
                Gizmos.DrawLine(point1, point2);
            }
        }
#endif
    }
}
