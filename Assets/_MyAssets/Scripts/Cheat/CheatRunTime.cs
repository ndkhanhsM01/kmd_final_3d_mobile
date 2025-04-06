
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatRunTime: MonoBehaviour
{
#if !ENABLE_CHEAT
    private void Awake()
    {
        Destroy(gameObject);
    }
#else
    [SerializeField] private SOSceneAsset[] scenes;
    [SerializeField] private GameObject panel;
    [SerializeField] private SOMcDefaultStats stats;
    [SerializeField] private SOBoolVariable godStatus;
    [SerializeField] private SOIntVariable sharedCoin;
    [SerializeField] private TMP_InputField inputSpeed;
    [SerializeField] private TMP_Dropdown dropDownScene;

    private Dictionary<string, Scene> DictScenePlayable = new();
    private float defaultSpeed;
    private void Start()
    {
        AddOptionsScene();
        panel.SetActive(false);
        defaultSpeed = stats.MoveSpeed;
    }
    private void OnEnable()
    {
        inputSpeed.text = stats.MoveSpeed.ToString();

    }

    public void ToggleGodMode()
    {
        godStatus.Value = !godStatus.Value;
    }
    public void CheatCoin()
    {
        sharedCoin.Value += 100;
    }
    public void SetSpeed()
    {
        try
        {
            float speed = float.Parse(inputSpeed.text);
            stats.SetSpeed(speed);
        }
        catch
        {

        }
    }
    public void ResetSpeed()
    {
        stats.SetSpeed(defaultSpeed);
    }
    private void AddOptionsScene()
    {
        int indexOptionSelected = -1;
        dropDownScene.ClearOptions();
        DictScenePlayable = new();
        for (int i = 0; i < scenes.Length; i++)
        {
            var s = scenes[i];
            var newOption = new TMP_Dropdown.OptionData();
            newOption.text = s.name;
            dropDownScene.options.Add(newOption);
        }
        dropDownScene.RefreshShownValue();
        if (indexOptionSelected >= 0) dropDownScene.value = indexOptionSelected;
    }
    public void GoScene()
    {
        int indexSelected = dropDownScene.value;
        LoadSceneManager.Instance.LoadSceneByAsset(scenes[indexSelected], true);
    }

#endif

}