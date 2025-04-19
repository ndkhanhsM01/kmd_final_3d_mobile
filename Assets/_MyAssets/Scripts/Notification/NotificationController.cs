using UnityEngine;
using UnityEngine.Android;
using Unity.Notifications.Android;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private AndroidNotiCustom androidNoti;

    [SerializeField] private DailyNoti[] dailyNoties;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        androidNoti.RequestAuthorization();
        androidNoti.RegisterNotificationChannel();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            AndroidNotificationCenter.CancelAllNotifications();

            //androidNoti.SendNotification("Test", "Hihi", 30);
            foreach (var notice in dailyNoties)
            {
                androidNoti.ScheduleDailyNotification(notice);
            }
        }
    }
    private void OnApplicationQuit()
    {

        AndroidNotificationCenter.CancelAllNotifications();

        foreach(var notice  in dailyNoties)
        {
            androidNoti.ScheduleDailyNotification(notice);
        }

    }
}

[System.Serializable]
public struct DailyNoti
{
    public string Title;
    public int Hour;
    public int Minute;
    [TextArea] public string Description;
}
