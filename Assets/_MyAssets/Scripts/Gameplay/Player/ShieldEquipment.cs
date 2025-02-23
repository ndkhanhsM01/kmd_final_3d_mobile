
using Cysharp.Threading.Tasks;
using MLib;
using System.Threading;
using UnityEngine;

public class ShieldEquipment: MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private SOBoolVariable godStatus;
    [SerializeField] private SOBoolEventChannel setEquipChannel;
    [SerializeField] private SpriteRenderer sprFill;

    private bool shieldEnabled => visual.activeSelf;
    private int paramFill = Shader.PropertyToID("_Arc1");
    private void Awake()
    {
        visual.SetActive(false);
        sprFill.SetActive(false);
    }

    private void OnEnable()
    {
        setEquipChannel.Register(OnSetEquip);
    }
    private void OnDisable()
    {
        setEquipChannel.Unregister(OnSetEquip);
    }

    private void OnSetEquip(bool value)
    {
        if (value)
        {
            EquipShield();
        }
        else
        {
            DestroyShield();
        }
    }

    private void EquipShield()
    {
        godStatus.Value = true;
        visual.SetActive(true);

        sprFill.SetActive(true);
        sprFill.sharedMaterial.SetFloat(paramFill, 0f);
    }
    private async void DestroyShield()
    {
        if (!shieldEnabled)
            return;
        visual.SetActive(false);

        float delay = 3f;
        float timer = 0f;
        while(timer < delay)
        {
            timer += Time.deltaTime;
            sprFill.sharedMaterial.SetFloat(paramFill, (timer / delay) * 360f);
            await UniTask.WaitForEndOfFrame();
        }

        sprFill.SetActive(false);
        godStatus.Value = false;
    }
}