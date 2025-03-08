
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOPrisonKeyReference", menuName = "Gameplay/SOPrisonKeyReference")]
public class SOPrisonKeyReference: ScriptableObject
{
    private HashSet<PrisonKey> keyCollection = new();
    private Dictionary<Prison, PrisonKeyPair> dictPrison = new();
    public void Renew()
    {
        keyCollection = new();
        dictPrison = new();
    }
    public void SetPrisonKeyPairs(PrisonKeyPair[] pairs)
    {
        foreach (var pair in pairs)
        {
            dictPrison.Add(pair.Prison, pair);
        }
    }
    public void AddKey(PrisonKey key)
    {
        keyCollection.Add(key);

        Debug.Log($"Add new key {key.name}");
    }
    public void ClearKeys()
    {
        keyCollection.Clear();
    }
    public bool TryUnlockPrison(Prison prison)
    {
        if (!dictPrison.TryGetValue(prison, out PrisonKeyPair prisonKeyPair))
        {
            Debug.LogError("Not found prison in dictionary");
            return false;
        }

        if (!keyCollection.Contains(prisonKeyPair.Key))
        {
            Debug.LogWarning("Require key was not collected yet");
            return false;
        }

        keyCollection.Remove(prisonKeyPair.Key);
        return true;
    }
    public bool CheckContainCorrectKey(Prison prison)
    {
        if (!dictPrison.TryGetValue(prison, out PrisonKeyPair prisonKeyPair))
        {
            Debug.LogError("Not found prison in dictionary");
            return false;
        }

        if (!keyCollection.Contains(prisonKeyPair.Key))
        {
            Debug.LogWarning("Require key was not collected yet");
            return false;
        }

        return true;
    }
}

[System.Serializable]
public class PrisonKeyPair
{
    [SerializeField] private Color color = Color.green;
    [SerializeField] private Prison prison;
    [SerializeField] private PrisonKey key;

    public Color Color => color;
    public Prison Prison => prison;
    public PrisonKey Key => key;
    public void Init()
    {
        prison.SetColor(color);
        key.SetColor(color);
    }
}
     