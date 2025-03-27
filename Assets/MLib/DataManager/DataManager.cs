using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;


namespace MLib
{
    public class DataManager : MSingleton<DataManager>
    {
        [SerializeField] private bool showDebug = true;
        [SerializeField] private SOIntVariable sharedCoin;
        [SerializeField] private string fileName = "SaveData.json";

        [HideInInspector] public static LocalData LocalData;
        protected override void Awake()
        {
            base.Awake();
            Load();
        }
        private void OnEnable()
        {
            sharedCoin.Register_OnValueChanged(OnCoinChanged);
        }
        private void OnDisable()
        {
            sharedCoin.Unregister_OnValueChanged(OnCoinChanged);
        }
        private void OnApplicationQuit()
        {
            Save();
        }
        private void OnApplicationFocus(bool focus)
        {
            Save();
        }
        public void RenewLevel()
        {
            LocalData.CurrentLevel = 0;
            Save();
        }

        public void Load()
        {
            // load file
            string path = Application.persistentDataPath + "/" + fileName;
            LocalData = MHelper.LoadDataFromFile<LocalData>(path);
            sharedCoin.Value = LocalData.Coin;
        }

        public void Save()
        {
            string path = Application.persistentDataPath + "/" + fileName;
            MHelper.SaveDataIntoFile(path, LocalData);
        }

        private void OnCoinChanged(int newValue)
        {
            LocalData.Coin = newValue;
        }
    }
}