using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MLib
{
    public class EditorConfigSO: ScriptableObject
    {
        public bool IsLoadLevel = true;
        public bool IsUseJoystick = true;

#if UNITY_EDITOR
        public SceneAsset[] SceneAssets;
#endif

        #region private properties
        [SerializeField] private string[] defineSymbolsToggle = new string[] {"ENABLE_CHEAT", "ENABLE_REMOVE_ADS"};

        #endregion

        #region public properties
        public string[] DefineSymbolsToggle => defineSymbolsToggle;
        #endregion
    }
}