using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    private void Awake()
    {
        instance = this;

#if UNITY_ANDROID
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Id = "reward",
            Name = "Rewards",
            Description = "Notification about ingame rewards",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
#endif
    }

    public void RegisterRewardReadyNotification(float fireTime)
    {
#if UNITY_ANDROID
        AndroidNotification notification = new AndroidNotification();

        notification.Title = LocaleHelper.GetString("Notification.RewardReady.Title");
        notification.Text = LocaleHelper.GetString("Notification.RewardReady.Text");
        notification.SmallIcon = "icon_small";
        notification.LargeIcon = "icon_large";
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddMilliseconds(fireTime);

        AndroidNotificationCenter.SendNotification(notification, "reward");
#endif
    }
}
