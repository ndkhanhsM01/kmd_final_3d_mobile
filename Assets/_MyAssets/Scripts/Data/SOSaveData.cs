

using MLib;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SOSaveData: ScriptableObject
{
    [Button]
    public abstract void Load();

    [Button]
    public abstract void Save();

    public virtual string GetFileName()
    {
        return GetType().ToString() + ".json";
    }
}