using Cysharp.Threading.Tasks;
using MLib;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MainCharacter mc;
    [SerializeField] private Room[] rooms;

    [SerializeField] private GatePair[] gatePairs;
    [SerializeField] private Hostage[] hostages;
    [SerializeField] private PrisonKeyPair[] prisonKeyPairs;

    public MainCharacter MC => mc;
    public GatePairStorage GatePairStorage { get; private set; }
    public Hostage[] Hostages => hostages;
    public PrisonKeyPair[] PrisonKeyPairs => prisonKeyPairs;

    public void BeginSetup()
    {
        GatePairStorage = new GatePairStorage(gatePairs);

        foreach (var pair in prisonKeyPairs)
            pair.Init();

        ShowDefaultRoom();
    }

    private async void ShowDefaultRoom()
    {
        await UniTask.WaitForEndOfFrame();
        rooms[0].Show();
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(i == 0);
        }
    }

#if UNITY_EDITOR
    [Button]
    private void FindElements()
    {
        hostages = GetComponentsInChildren<Hostage>(true);
        rooms = GetComponentsInChildren<Room>(true);
        mc = GetComponentInChildren<MainCharacter>(true);

        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach(var pair in gatePairs)
        {
            Gizmos.DrawLine(pair.Gate1.transform.position + Vector3.up
                            , pair.Gate2.transform.position + Vector3.up);
        }

        foreach(var pair in prisonKeyPairs)
        {
            Gizmos.color = pair.Color;
            Gizmos.DrawLine(pair.Key.transform.position + Vector3.up
                            , pair.Prison.transform.position + Vector3.up);
        }
    }
#endif

}

[System.Serializable]
public class GatePair
{
    [SerializeField] private Gate gate1;
    [SerializeField] private Gate gate2;
    public Gate Gate1 => gate1;
    public Gate Gate2 => gate2;
}

public class GatePairStorage
{
    private Dictionary<Gate, GatePair> dictGates;
    public GatePairStorage(params GatePair[] pairs)
    {
        dictGates = new();
        foreach (var p in pairs)
        {
            dictGates.TryAdd(p.Gate1, p);
            dictGates.TryAdd(p.Gate2, p);
        }
    }
    public GatePair GetPair(Gate key)
    {
        return dictGates[key];
    }
    public Gate GetPartner(Gate key)
    {
        GatePair pair = dictGates[key];
        return key == pair.Gate1 ? pair.Gate2 : pair.Gate1;
    }
}
