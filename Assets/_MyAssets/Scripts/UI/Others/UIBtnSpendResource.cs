
using MLib;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBtnSpendResource: MonoBehaviour
{
    [SerializeField] private SOIntVariable sharedResource;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text tmpValue;
    [SerializeField] private Color colorOn = Color.green;
    [SerializeField] private Color colorOff = Color.red;

    public void SetRequireValue(int value)
    {
        bool enough = value <= sharedResource.Value;
        tmpValue.text = value.ToString();

        button.interactable = enough;
        tmpValue.color = enough ? colorOn : colorOff;
    }
    public void AddListener(UnityAction callback)
    {
        button.AddListener(callback);
    }
    public void RemoveListener(UnityAction callback)
    {
        button.RemoveListener(callback);
    }
}