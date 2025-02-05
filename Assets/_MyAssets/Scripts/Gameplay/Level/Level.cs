using MLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MainCharacter mc;
    [SerializeField] private LevelViewport viewport;

    [SerializeField] private GatePair[] gatePairs;
    [SerializeField] private Hostage[] hostages;
    [SerializeField] private PrisonKeyPair[] prisonKeyPairs;

    public MainCharacter MC => mc;
    public GatePairStorage GatePairStorage { get; private set; }
    public Hostage[] Hostages => hostages;
    public PrisonKeyPair[] PrisonKeyPairs => prisonKeyPairs;
    private void Awake()
    {
        GatePairStorage = new GatePairStorage(gatePairs);
    }

#if UNITY_EDITOR
    [MButton]
    private void FindElements()
    {
        hostages = GetComponentsInChildren<Hostage>();


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
