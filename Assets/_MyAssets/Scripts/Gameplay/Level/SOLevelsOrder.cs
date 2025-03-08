
using MLib;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelOrder", menuName = "LevelOrder")]
public class SOLevelsOrder: ScriptableObject
{
    [SerializeField] private SOSceneAsset[] levels;

    public int TotalLevels => levels.Length;
    public SOSceneAsset[] Levels => levels;
    public SOSceneAsset GetLevelSceneAsset(int levelIndex)
    {
        if (levels.IsOutOfRange(levelIndex))
            return null;

        return levels[levelIndex];
    }
}