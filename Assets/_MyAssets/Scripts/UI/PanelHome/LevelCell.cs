

using MLib;
using TMPro;
using UnityEngine;

public class LevelCell: MonoBehaviour
{
    [SerializeField] private TMP_Text tmpNumber;
    [SerializeField] private GameObject goHighlight;
    [SerializeField] private GameObject goLocked;
    [SerializeField] private GameObject goDone;

    private int curLevelReached => DataManager.LocalData.CurrentLevel;
    public void Setup(int level)
    {
        tmpNumber.text = $"{level + 1}";
        goHighlight.SetActive(level == curLevelReached);
        goLocked.SetActive(curLevelReached < level);
        goDone.SetActive(curLevelReached > level);
    }
}