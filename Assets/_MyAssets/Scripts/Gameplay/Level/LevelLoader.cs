
using UnityEngine;

public class LevelLoader: MonoBehaviour
{
    [SerializeField] private Transform mapHolder;
    private string pathResource = "Levels";
    public Level LoadLevel()
    {
        Level cloneLevel = Resources.Load<Level>(pathResource + "/Level_0");

        Instantiate(cloneLevel, mapHolder);
        return cloneLevel;
    }
}