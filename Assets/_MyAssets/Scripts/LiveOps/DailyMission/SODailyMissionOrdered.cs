
using System.Collections.Generic;
using MLib;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "DailyMissionOrdered", menuName = "LiveOps/DailyMission/Missions Ordered")]
public class SODailyMissionOrdered: ScriptableObject
{
    [SerializeField] private SOMissionGroup[] missionGroups;

    public SOMissionGroup[] MissionGroups => missionGroups;
    public void UpdateProgress(Dictionary<string, int> progress, HashSet<string> claimed)
    {
        foreach(var gr in MissionGroups)
        {
            var mission = gr.CurrentMission;
            if (mission == null) continue;

            if (progress.TryGetValue(mission.Key, out int current))
                mission.Init(current);
            else
                mission.Init(0);
            mission.Claimed = claimed.Contains(mission.Key);
        }
    }
    public void SetCurrentMissions(List<int> indexsCurrentMission)
    {
        if (indexsCurrentMission.Count != missionGroups.Length)
        {
            Debug.LogWarning("Amount of indexs invalid!!");
            return;
        }

        for (int i = 0; i < missionGroups.Length; i++)
        {
            var gr = missionGroups[i];
            gr.SetCurrentMission(indexsCurrentMission[i]);
        }
    }
    public List<int> GetIndexCurrentMissions()
    {
        List<int> result = new();
        foreach (var gr in missionGroups)
            result.Add(gr.IndexCurMission);

        return result;
    }
    public void RandomMissionForeachGroup(List<int> listExcept)
    {
        bool isFirstTime = listExcept.Count == 0;

        if (isFirstTime)
        {
            foreach (var gr in MissionGroups)
            {
                gr.SetRandom(null);
            }
        }
        else if(listExcept.Count == missionGroups.Length)
        {
            for (int i = 0; i < listExcept.Count; i++)
            {
                int indexExcept = listExcept[i];
                missionGroups[i].SetRandom(indexExcept);
            }
        }
        else
        {
            Debug.LogWarning("Amount of indexExcept invalid!!");
        }
    }
}