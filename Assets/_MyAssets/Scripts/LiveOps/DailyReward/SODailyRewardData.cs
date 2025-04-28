
using MLib;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyRewardData", menuName = "LiveOps/DailyReward/SaveData")]
public class SODailyRewardData: SOSaveDataGeneric<DailyRewardData>
{
    public override void Load()
    {
        base.Load();
        CheckDailyReward();
    }
    public override void Save()
    {
        saveValue.lastTimeLogin = TimeHelper.UnixTimeNow;

        base.Save();
    }
    public void ResetStreak()
    {
        saveValue = new();
    }
    public void IncStreak(int step)
    {
        saveValue.loginStreak += step;
    }
    public void MarkDayClaimed(int day)
    {
        saveValue.daysClaimed.Add(day);
    }
    public int GetStreak()
    {
        return saveValue.loginStreak;
    }
    public bool CheckDayClaimed(int day)
    {
        return saveValue.daysClaimed.Contains(day);
    }
    private void CheckDailyReward()
    {
        DateTime now = DateTime.Today;
        DateTime lastLogin = TimeHelper.UnixTimeStampToDateTime(saveValue.lastTimeLogin);

        double diffDay = (now.Date - lastLogin.Date).TotalDays;

        if (diffDay < 0)    // user cheated
            return;

        if (diffDay==0)     // same day
            return;

        if (diffDay > 1)
            ResetStreak();
        else if(diffDay == 1)
        {
            IncStreak(1);
        }
    }
}

[System.Serializable]
public class DailyRewardData
{
    public int loginStreak = 1;
    public double lastTimeLogin = -1;
    public HashSet<int> daysClaimed = new();

    public DailyRewardData()
    {
        loginStreak = 1;
        lastTimeLogin = -1;
        daysClaimed = new();
    }
}