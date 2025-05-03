
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionOrdered", menuName = "LiveOps/DailyMission/Missions Ordered")]
public class SODailyMissionOrdered: ScriptableObject
{
    [SerializeField] private SOMission[] missions;

    public SOMission[] Missions => missions;
    public void UpdateProgress(Dictionary<string, int> progress, HashSet<string> claimed)
    {
        foreach(var mission in missions)
        {
            if (progress.TryGetValue(mission.Key, out int current))
                mission.Init(current);
            else
                mission.Init(0);
            mission.Claimed = claimed.Contains(mission.Key);
        }
    }
}