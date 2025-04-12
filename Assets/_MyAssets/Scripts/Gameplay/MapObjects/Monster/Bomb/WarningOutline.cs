
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Monster.Bomb
{
    public class WarningOutline : MonoBehaviour
    {
        [SerializeField] private int outlineSlot = 1;
        [SerializeField] private float duration = 0.15f;
        [SerializeField] private RangeFloat thickness;
        [SerializeField] private SkinnedMeshRenderer mesh;

        private int paramThickness = Shader.PropertyToID("_Outline_Thickness");

        private Material matIns;
        private Tween t;
        private void Start()
        {
            DisableWarning();
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            PlayAnim();
        }
        private void OnDisable()
        {
            if (t != null)
                t.Kill();

            DisableWarning();
        }
        private void DisableWarning()
        {
            if (!matIns)
                matIns = mesh.materials[outlineSlot];

            matIns.SetFloat(paramThickness, 0f);
        }
        public void PlayAnim()
        {
            if(!matIns)
                matIns = mesh.materials[outlineSlot];

            t = DOVirtual.Float(thickness.min, thickness.max, duration, (value) =>
            {
                matIns.SetFloat(paramThickness, value);
            }).SetLoops(-1, LoopType.Yoyo);
        }
    }
}