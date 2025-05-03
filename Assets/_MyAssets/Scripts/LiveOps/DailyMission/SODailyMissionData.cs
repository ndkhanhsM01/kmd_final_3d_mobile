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
        if(last.Day != DateTime.Now.Day)
            saveValue.Renew();

        missionOrdered.UpdateProgress(saveValue.missionsProgress, saveValue.missionsClaimed);
    }
    public override void Save()
    {
        saveValue.UpdateProgress(missionOrdered.Missions);

        base.Save();
    }
}

[System.Serializable]
public class DailyMissionData
{
    public double timeStart = -1;
    public Dictionary<string, int> missionsProgress = new();
    public HashSet<string> missionsClaimed = new();

    public void Renew()
    {
        timeStart = TimeHelper.UnixTimeNow;
        missionsProgress = new();
        missionsClaimed = new();
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