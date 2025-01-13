using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;


namespace MLib
{
    public class DataManager : MSingleton<DataManager>
    {
        [SerializeField] private bool showDebug = true;
        [SerializeField] private string fileName = "SaveData.json";

        [HideInInspector] public LocalData LocalData;
        protected override void Awake()
        {
            base.Awake();
            Load();
        }
        private void OnApplicationQuit()
        {
            Save();
        }
        private void OnApplicationFocus(bool focus)
        {
            Save();
        }

        public void Load()
        {
            // load file
            string path = Application.persistentDataPath + "/" + fileName;
            LocalData = MHelper.LoadDataFromFile<LocalData>(path);
        }

        public void Save()
        {
            string path = Application.persistentDataPath + "/" + fileName;
            MHelper.SaveDataIntoFile(path, LocalData);
        }
    }
}