using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MainCharacter mc;
    [SerializeField] private LevelViewport viewport;

    [SerializeField] private GatePair[] gatePairs;

    public MainCharacter MC => mc;
    public GatePairStorage GatePairStorage { get; private set; }

    private void Awake()
    {
        GatePairStorage = new GatePairStorage(gatePairs);
    }
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
