
using MLib;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelOrder", menuName = "LevelOrder")]
public class SOLevelsOrder: ScriptableObject
{
    [SerializeField] private string pathResource = "Levels";
    [SerializeField] private string[] levelNamesOrder = new string[1] {"Level_0"};

    public int TotalLevels => levelNamesOrder.Length;
    public string[] LevelNamesOrder => levelNamesOrder;
    public Level GetLevelPrefab(int levelIndex)
    {
        if (levelNamesOrder.IsOutOfRange(levelIndex))
            return null;

        Level level = Resources.Load<Level>(pathResource + $"/{levelNamesOrder[levelIndex]}");
        return level;
    }
}