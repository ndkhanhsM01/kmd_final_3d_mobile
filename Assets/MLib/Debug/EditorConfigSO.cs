using UnityEngine;

namespace MLib
{
    public class EditorConfigSO: ScriptableObject
    {
        public bool IsLoadLevel = true;
        public bool IsUseJoystick = true;

        #region private properties
        [SerializeField] private string[] defineSymbolsToggle = new string[] {"ENABLE_CHEAT", "ENABLE_REMOVE_ADS"};

        #endregion

        #region public properties
        public string[] DefineSymbolsToggle => defineSymbolsToggle;
        #endregion
    }
}