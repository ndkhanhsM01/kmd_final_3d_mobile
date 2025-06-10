
using MLib;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private Button btnGodMode;

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
        btnGodMode.AddListener(OnClick_GodMode);
        UpdateColorBtnGodMode();
    }
    private void OnDisable()
    {
        btnGodMode.RemoveListener(OnClick_GodMode);
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
        inputSpeed.text = stats.MoveSpeed.ToString();
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

    private void OnClick_GodMode()
    {
        godStatus.Value = !godStatus.Value;
        UpdateColorBtnGodMode();
    }
    public void UpdateColorBtnGodMode()
    {
        btnGodMode.image.color = godStatus.Value ? Color.green : Color.red;

    }
#endif

}