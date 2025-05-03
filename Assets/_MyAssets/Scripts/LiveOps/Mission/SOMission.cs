using MLib;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using ReadOnly = Sirenix.OdinInspector.ReadOnlyAttribute;


[CreateAssetMenu(fileName = "Mission_", menuName = "LiveOps/Mission")]
public class SOMission : ScriptableObject
{
    [Header("Description")]
    [SerializeField] protected string title = "Simple Mission";

    [Tooltip("<x> will be replaced by <require number>")]
    [SerializeField, TextArea] protected string description = "Try it!!";

    [Header("Values")]
    [SerializeField] protected int require;
    [SerializeField] protected RewardInGame[] rewards;
    [SerializeField] protected SOIntVariable sharedCurrent;
    private static Action<SOMission> onSuccess;

    public bool Claimed { get; set; }
    public bool IsReached => sharedCurrent.Value >= require;
    public RewardInGame[] Rewards => rewards;
    public string Key => $"{name}_{require}";
    public int Require => require;

    private int preCurrent;
    public int Current => sharedCurrent.Value;
    public void Init(int current)
    {
        preCurrent = current;
        sharedCurrent.Value = current;
        sharedCurrent.Unregister_OnValueChanged(OnShareCurrentChanged);
        sharedCurrent.Register_OnValueChanged(OnShareCurrentChanged);
    }
    private void OnShareCurrentChanged(int newValue)
    {
        bool isReachedBefore = preCurrent >= require;
        if (!isReachedBefore && IsReached)
            onSuccess?.Invoke(this);

        preCurrent = newValue;
    }
    public void ClaimRewards()
    {
        foreach (RewardInGame reward in rewards)
            reward.Receive();

        Claimed = true;
    }
    public string GetTitle() => title;
    public string GetDescription()
    {
        return description.Replace(" x ", $"<color=#00FF00> {require} </color>");
    }
    public static void Register_Success(Action<SOMission> callback)
    {
        onSuccess += callback;
    }
    public static void Unregister_Success(Action<SOMission> callback)
    {
        onSuccess -= callback;
    }

#if UNITY_EDITOR
    [Button]
    private void FakeSuccess()
    {
        onSuccess?.Invoke(this);
    }
#endif
}
