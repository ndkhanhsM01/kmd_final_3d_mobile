using UnityEngine.Android;
using UnityEngine;
using Unity.Notifications;
using Unity.Notifications.Android;
using System;

public class AndroidNotiCustom: MonoBehaviour
{
    [SerializeField] private string id = "default_channel";
    [SerializeField] private string nameNoti = "Default Channel";
    [SerializeField] private string permissionName = "android.permission.POST_NOTIFICATIONS";

    public void RequestAuthorization()
    {
        bool unauthorized = Application.platform == RuntimePlatform.Android &&
                            SystemInfo.operatingSystem.Contains("13") &&
                            !Permission.HasUserAuthorizedPermission(permissionName);
        Debug.Log("Un-Permission: " + unauthorized);
        if (unauthorized)
        {
            Permission.RequestUserPermission(permissionName);
        }
    }

    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = id,
            Name = nameNoti,
            Importance = Importance.High,
            Description = "Test 00"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, int fireTimeInSec)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(fireTimeInSec);
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, id);
        Debug.Log("XXPushed Noti");
    }

    public void ScheduleDailyNotification(DailyNoti dailyNoti)
    {
        var now = DateTime.Now;
        var notificationTime = new DateTime(now.Year, now.Month, now.Day, dailyNoti.Hour, dailyNoti.Minute, 0);

        if (notificationTime < now)
        {
            notificationTime = notificationTime.AddDays(1);
        }

        var notification = new AndroidNotification()
        {
            Title = dailyNoti.Title,
            Text = dailyNoti.Description,
            SmallIcon = "icon_0",
            LargeIcon = "icon_1",
            FireTime = notificationTime,
            RepeatInterval = TimeSpan.FromHours(1),
            Style = NotificationStyle.BigTextStyle
        };

        int idResult = AndroidNotificationCenter.SendNotification(notification, id);
        var status = AndroidNotificationCenter.CheckScheduledNotificationStatus(idResult);
        Debug.Log("XXNoti Status: " + status);
    }
}