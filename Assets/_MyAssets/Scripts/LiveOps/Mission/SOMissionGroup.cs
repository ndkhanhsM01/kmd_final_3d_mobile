using MLib;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "MissionGroup_", menuName = "LiveOps/MissionGroup")]
public class SOMissionGroup : ScriptableObject
{
    [SerializeField] private SOMission[] missions;

    public SOMission CurrentMission { get; private set; }
    public int IndexCurMission { get; private set; }    

    public void SetCurrentMission(int index)
    {
        CurrentMission = missions[index];
        IndexCurMission = index;
    }

    public void SetRandom(int exceptIndex)
    {
        var except = missions[exceptIndex];
        if(except == null)
        {
            Debug.LogWarning("Invalid except!!");
            return;
        }

        SetRandom(except);
    }
    public void SetRandom(SOMission except)
    {
        IndexCurMission = -1;
        if (missions == null || missions.Length == 0)
        {
            Debug.LogWarning("Empty group!!");
            CurrentMission = null;
        }

        bool hasOnlyOne = missions.Length == 1 && missions[0] == except;
        if (hasOnlyOne)
        {
            IndexCurMission = 0;
            CurrentMission = missions[0];
        }

        int rnd = Random.Range(0, missions.Length);
        for(int i=0; i< missions.Length; i++)
        {
            rnd--;
            if (rnd >= 0)
                continue;

            var mission = missions[i];
            if (mission == except)
                continue;

            CurrentMission = mission;
            IndexCurMission = i;
            break;
        }
    }
}
