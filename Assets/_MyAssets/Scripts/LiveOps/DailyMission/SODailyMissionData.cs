using UnityEngine;
using MLib;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "DailyMissionData", menuName = "LiveOps/DailyMission/SaveData")]
public class SODailyMissionData : SOSaveDataGeneric<DailyMissionData>
{
    [SerializeField] private SODailyMissionOrdered missionOrdered;
    public override void Load()
    {
        base.Load();
        DateTime last = TimeHelper.UnixTimeStampToDateTime(saveValue.timeStart);

        bool newDay = last.Day != DateTime.Now.Day;
        if (newDay)
        {
            missionOrdered.RandomMissionForeachGroup(saveValue.indexsCurrentMissions);
            List<int> newIndexCurMissions = missionOrdered.GetIndexCurrentMissions();
            saveValue.Renew(newIndexCurMissions);
        }
        else
        {
            missionOrdered.SetCurrentMissions(saveValue.indexsCurrentMissions);
        }

        missionOrdered.UpdateProgress(saveValue.missionsProgress, saveValue.missionsClaimed);
    }
    public override void Save()
    {
        foreach (var gr in missionOrdered.MissionGroups)
        {
            saveValue.UpdateProgress(gr.CurrentMission);
        }

        base.Save();
    }
}

[System.Serializable]
public class DailyMissionData
{
    public double timeStart = -1;
    public List<int> indexsCurrentMissions = new();
    public Dictionary<string, int> missionsProgress = new();
    public HashSet<string> missionsClaimed = new();

    public void Renew(List<int> newIndexCurMissions)
    {
        timeStart = TimeHelper.UnixTimeNow;
        missionsProgress = new();
        missionsClaimed = new();
        indexsCurrentMissions = newIndexCurMissions;
    }

    public void UpdateProgress(SOMission mission)
    {
        if (missionsProgress.ContainsKey(mission.Key))
            missionsProgress[mission.Key] = mission.Current;
        else
            missionsProgress.Add(mission.Key, mission.Current);

        if (mission.Claimed)
            missionsClaimed.Add(mission.Key);
    }
    public void UpdateProgress(SOMission[] missions)
    {
        foreach(SOMission mission in missions)
        {
            if (missionsProgress.ContainsKey(mission.Key))
                missionsProgress[mission.Key] = mission.Current;
            else 
                missionsProgress.Add(mission.Key, mission.Current);

            if(mission.Claimed)
                missionsClaimed.Add(mission.Key);
        }
    }
}