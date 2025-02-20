
using MLib;
using System;
using UnityEngine;

public class LevelLoader: MonoBehaviour
{
    [SerializeField] private Transform mapHolder;
    private string pathResource = "Levels";

    [SerializeField] private EditorConfigSO editorConfig;
    public static Action<Level> OnNewLevelLoaded;
    public Level LoadLevel()
    {
        Level level = Resources.Load<Level>(pathResource + "/Level_0");

#if UNITY_EDITOR
        if (editorConfig.LevelTest)
        {
            level = editorConfig.LevelTest;
        }
#endif

        var cloneLevel = Instantiate(level, mapHolder);
        MHelper.FocusGameobject(cloneLevel.gameObject);
        return cloneLevel;
    }
}