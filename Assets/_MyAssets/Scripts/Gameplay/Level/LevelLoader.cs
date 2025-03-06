
using MLib;
using System;
using UnityEngine;

public class LevelLoader: MonoBehaviour
{
    [SerializeField] private SOLevelsOrder levelsOrder;
    [SerializeField] private Transform mapHolders;

    [SerializeField] private EditorConfigSO editorConfig;
    public static Action<Level> OnNewLevelLoaded;
/*    public Level LoadLevel()
    {
        int index = DataManager.Instance.LocalData.CurrentLevel;
        var level = levelsOrder.GetLevelPrefab(index);
#if UNITY_EDITOR
        if (editorConfig.LevelTest)
        {
            level = editorConfig.LevelTest;
        }
#endif

        var cloneLevel = Instantiate(level, mapHolders);
        MHelper.FocusGameobject(cloneLevel.gameObject);
        return cloneLevel;
    }*/
}