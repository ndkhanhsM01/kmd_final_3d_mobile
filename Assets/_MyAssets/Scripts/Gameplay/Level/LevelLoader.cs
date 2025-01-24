
using MLib;
using UnityEngine;

public class LevelLoader: MonoBehaviour
{
    [SerializeField] private Transform mapHolder;
    private string pathResource = "Levels";
    public Level LoadLevel()
    {
        Level level = Resources.Load<Level>(pathResource + "/Level_0");

        var cloneLevel = Instantiate(level, mapHolder);
        MHelper.FocusGameobject(cloneLevel.gameObject);
        return cloneLevel;
    }
}