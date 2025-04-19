
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimateHostage: MonoBehaviour
{
    [SerializeField] private GameObject chain;
    [SerializeField] private Animator animator;
    [SerializeField] Rig rig;


    [Button]
    public async void Rescue()
    {
        chain.SetActive(false);
        DOVirtual.Float(1f, 0f, 0.25f, (value) =>
        {
            rig.weight = value;
        });

        await UniTask.WaitForSeconds(0.25f);
        animator.SetTrigger("release");
    }
}