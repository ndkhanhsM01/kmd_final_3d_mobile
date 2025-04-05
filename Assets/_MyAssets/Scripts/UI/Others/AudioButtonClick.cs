
using MLib;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonClick : MonoBehaviour
{
    [SerializeField, Required] private Button button;
    [SerializeField] private SOAudio config;

    private void Reset()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.AddListener(config.Play);
    }
    private void OnDisable()
    {
        button.RemoveListener(config.Play);
    }
}