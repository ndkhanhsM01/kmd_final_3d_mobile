
using UnityEngine;

namespace Monster
{
    public class VisionAttacker: MonoBehaviour
    {
        [SerializeField] private float length = 3f;
        [SerializeField] private float angle = 60f;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private Material visionConeMaterial;
        [SerializeField] private LayerMask visionObstructingLayer;
        [SerializeField] private int visionConeResolution = 50;
        [SerializeField] private GameObject goMesh;

        private Mesh visionConeMesh;
        private MeshFilter meshFilter;
        private float angleRad;
        private Vector3 forward => transform.forward;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                goMesh.SetActive(value);
            }
        }
        void Start()
        {
            goMesh.AddComponent<MeshRenderer>().material = visionConeMaterial;
            meshFilter = goMesh.AddComponent<MeshFilter>();
            visionConeMesh = new Mesh();
            angleRad = angle * Mathf.Deg2Rad;
        }


        void LateUpdate()
        {
            if (!IsActive)
            {
                return;
            }
            DrawVisionCone();
        }

        /// <summary>
        /// this method creates the vision cone mesh
        /// </summary>
        private void DrawVisionCone()
        {
            int[] triangles = new int[(visionConeResolution - 1) * 3];
            Vector3[] Vertices = new Vector3[visionConeResolution + 1];
            Vertices[0] = Vector3.zero;
            float Currentangle = -angleRad / 2;
            float angleIcrement = angleRad / (visionConeResolution - 1);
            float Sine;
            float Cosine;

            for (int i = 0; i < visionConeResolution; i++)
            {
                Sine = Mathf.Sin(Currentangle);
                Cosine = Mathf.Cos(Currentangle);
                Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
                Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
                if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, length, visionObstructingLayer))
                {
                    Vertices[i + 1] = VertForward * hit.distance;
                }
                else
                {
                    Vertices[i + 1] = VertForward * length;
                }


                Currentangle += angleIcrement;
            }
            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
            }
            visionConeMesh.Clear();
            visionConeMesh.vertices = Vertices;
            visionConeMesh.triangles = triangles;
            meshFilter.mesh = visionConeMesh;
        }

        public bool CheckInside(Vector3 point)
        {
            Vector3 center = transform.position + offset;
            center.y = point.y;

            bool isCloseEnough = Vector3.Distance(center, point) <= length;
            if (!isCloseEnough)
                return false;

            Vector3 directionToPoint = point - center;
            float angleToPoint = Vector3.Angle(forward, directionToPoint);
            if (angleToPoint <= angle / 2f)
                return true;
            else
                return false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 center = transform.position + offset;
            Gizmos.color = Color.red;
            DebugUtil.DrawGizmoArc(center, forward , transform.up, length, angle);

            Vector3 direction1 = Quaternion.Euler(0f, angle / 2f, 0f) * forward;
            Vector3 direction2 = Quaternion.Euler(0f, -angle / 2f, 0f) * forward;
            Gizmos.DrawLine(center, direction1 * length + center);
            Gizmos.DrawLine(center, direction2 * length + center);
        }
#endif
    }
}