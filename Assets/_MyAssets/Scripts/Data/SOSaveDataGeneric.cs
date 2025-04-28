

using MLib;
using Sirenix.OdinInspector;
using System.Diagnostics;
using UnityEngine;
using ReadOnly = Sirenix.OdinInspector.ReadOnlyAttribute;

public abstract class SOSaveDataGeneric<T> : SOSaveData where T : new()
{

    [Space(20f)]
    [InfoBox("Save value will be loaded at runtime", InfoMessageType.Warning)]
    [SerializeField, PropertyOrder(1000)] private bool showDebug = false;
    [SerializeField, PropertyOrder(1001)] protected bool initDefault = true;
    [SerializeField, PropertyOrder(1002), ReadOnly] protected T saveValue = new();

    public T SaveValue => saveValue;

    [PropertyOrder(1003)]
    [Button]
    public override void Load()
    {
        string path = Application.persistentDataPath + "/" + GetFileName();
        saveValue = MHelper.LoadDataFromFile<T>(path);
        if (initDefault && saveValue == null)
        {
            InitValue();
            Save();
        }
    }

    [PropertyOrder(1004)]
    [Button]
    public override void Save()
    {
        string path = Application.persistentDataPath + "/" + GetFileName();
        MHelper.SaveDataIntoFile(path, saveValue);
    }
    public override string GetFileName()
    {
        return typeof(T).ToString() + ".json";
    }

    protected virtual void InitValue()
    {
        saveValue = new();
    }

#if UNITY_EDITOR
    [PropertyOrder(1005)]
    [Button]
    private void OpenInDisk()
    {
        string path = Application.persistentDataPath;
        if (System.IO.Directory.Exists(path))
        {
            string windowsPath = path.Replace("/", "\\");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = windowsPath,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
        else
        {
            UnityEngine.Debug.LogError("Folder path does not exist: " + path);
        }
    }
#endif
}