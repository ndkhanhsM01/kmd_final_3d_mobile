
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
    [SerializeField] private GameObject on;
    [SerializeField] private GameObject off;

    public void SetRequireValue(int value)
    {
        bool enough = value <= sharedResource.Value;
        tmpValue.text = value.ToString();

        button.interactable = enough;
        on.SetActive(enough);
        off.SetActive(!enough);
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