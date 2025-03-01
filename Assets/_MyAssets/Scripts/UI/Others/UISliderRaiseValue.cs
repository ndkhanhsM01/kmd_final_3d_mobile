
using UnityEngine;
using UnityEngine.UI;

public class UISliderRaiseValue: MonoBehaviour
{
    [SerializeField] private SOFloatEventChannel channel;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        channel.Raise(value);
    }

    public void SetValue(float value)
    {
        slider.value = value;
        channel.Raise(value);
    }
}