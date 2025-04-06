
using Cysharp.Threading.Tasks;
using MLib;
using UnityEngine;

public class GodStatusHandler: MonoBehaviour
{
    [SerializeField] private SOBoolVariable godStatus;
    [SerializeField] private SpriteRenderer sprFill;

    private int paramFill = Shader.PropertyToID("_Arc1");

    private void Start()
    {
        sprFill.SetActive(false);
    }
    public void Active()
    {
        godStatus.Value = true;
        sprFill.SetActive(true);
        sprFill.sharedMaterial.SetFloat(paramFill, 0f);
    }
    public async void Active(float duration)
    {
        Active();
        await UniTask.WaitForSeconds(duration);
        Deactive();
    }
    public async void Deactive()
    {
        float delay = 3f;
        float timer = 0f;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            sprFill.sharedMaterial.SetFloat(paramFill, (timer / delay) * 360f);
            await UniTask.WaitForEndOfFrame();
        }

        sprFill.SetActive(false);
        godStatus.Value = false;
    }
}