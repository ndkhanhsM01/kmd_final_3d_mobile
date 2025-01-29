using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLib
{
    public class MUIManager : MSingleton<MUIManager>
    {
        private Dictionary<string, MPanel> dictPanels = new();
        protected override void Awake()
        {
            base.Awake();

            LoadAllPanels();
        }

        private void LoadAllPanels()
        {
            dictPanels = new();

            MPanel[] allPanels = GetComponentsInChildren<MPanel>(true);
            foreach (MPanel panel in allPanels)
            {
                dictPanels.Add(panel.GetType().ToString(), panel);
            }
        }

        public T GetPanel<T>() where T : MPanel
        {
            if(dictPanels.TryGetValue(typeof(T).ToString(), out MPanel panel))
            {
                return panel as T;
            }
            else
            {
                return null;
            }
        }
        public void ShowPanel<T>() where T : MPanel
        {
            if (dictPanels.TryGetValue(typeof(T).ToString(), out MPanel panel))
            {
                panel.Show();
            }
            else
            {
                Debug.LogError($"Panel <{typeof(T)}> not found");
            }
        }
        public void HidePanel<T>() where T : MPanel
        {
            if (dictPanels.TryGetValue(typeof(T).ToString(), out MPanel panel))
            {
                panel.Hide();
            }
            else
            {
                Debug.LogError($"Panel <{typeof(T)}> not found");
            }
        }

#if UNITY_EDITOR
        [MButton]
        private void Editor_HideAll()
        {
            MPanel[] allPanels = GetComponentsInChildren<MPanel>(true);
            foreach (MPanel panel in allPanels)
            {
                panel.Hide();
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}