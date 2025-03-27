
using TMPro;
using UnityEngine;

public class UICoinValue: MonoBehaviour
{
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private TMP_Text tmp;

    private void OnEnable()
    {
        sharedCoin.Register_OnValueChanged(OnCoinChanged);
    }
    private void OnDisable()
    {

        sharedCoin.Unregister_OnValueChanged(OnCoinChanged);
    }

    private void OnCoinChanged(int newValue)
    {
        UpdateTextValue();
    }

    public void UpdateTextValue()
    {
        tmp.text = sharedCoin.Value.ToString();
    }
}