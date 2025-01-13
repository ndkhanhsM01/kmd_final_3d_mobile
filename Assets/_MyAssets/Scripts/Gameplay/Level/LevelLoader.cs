
using UnityEngine;

public class LevelLoader: MonoBehaviour
{
    private string pathResource = "Levels";
    public Level LoadLevel()
    {
        Level cloneLevel = Resources.Load<Level>(pathResource + "/Level_0");

        Instantiate(cloneLevel, null);
        return cloneLevel;
    }
}