using UnityEngine;
using UnityEngine.Events;

public class UIReviveChecker : MonoBehaviour
{
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private SOIntVariable sharedCoinRevive;
    [SerializeField] private UnityEvent evtEnoughCoin;
    [SerializeField] private UnityEvent evtNotEnoughCoin;

    private void OnEnable()
    {
        sharedCoin.Register_OnValueChanged(OnCoinChanged);
        OnCoinChanged(sharedCoin.Value);
    }
    private void OnDisable()
    {
        sharedCoin.Unregister_OnValueChanged(OnCoinChanged);
    }

    private void OnCoinChanged(int coin)
    {
        bool enough = coin >= sharedCoinRevive.Value;
        if (enough)
        {
            evtEnoughCoin?.Invoke();
        }
        else
        {
            evtNotEnoughCoin?.Invoke();
        }
    }
}
