using System.Collections.Generic;
using UnityEngine.Analytics;

public static class AnalyticsHelper
{
    public enum DeadReason
    {
        Fall_off,
        Obstacle_collide
    }

    public static void OnPlayerDead(int score, DeadReason reason)
    {
        Analytics.CustomEvent("Player Dead", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "score", score },
            { "reason", reason.ToString() }
        });
    }

    public static void OnObstacleCollide(ObstacleType obstacleType)
    {
        Analytics.CustomEvent("Obstacle Collide", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "obstacle type", obstacleType.ToString() }
        });
    }

    public static void OnWindowOppened(WindowType windowType)
    {
        Analytics.CustomEvent("Obstacle Collide", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "window type", windowType.ToString() }
        });
    }

    public static void OnSkinSet(SkinType skinType)
    {
        Analytics.CustomEvent("Skin Set", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "skin type", skinType.ToString() }
        });
    }

    public static void OnSkinBought(SkinType skinType)
    {
        Analytics.CustomEvent("Shop Item Bought", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "skin type", skinType.ToString() }
        });
    }

    public static void OnAdWatch(RewardType rewardType, int value = 0)
    {
        Analytics.CustomEvent("Watch Ad", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "reward type", rewardType.ToString() },
            { "value",  value }
        });
    }

    public static void OnDailyRewardGet(int value)
    {
        Analytics.CustomEvent("Get Daily Reward", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "value",  value }
        });
    }

    public static void OnMusicEnabled(bool enabled)
    {
        Analytics.CustomEvent("Music Enabled", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "enabled", enabled }
        });
    }

    public static void OnSoundsEnabled(bool enabled)
    {
        Analytics.CustomEvent("Sounds Enabled", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "enabled", enabled }
        });
    }

    public static void OnCoinPickUp(int value)
    {
        Analytics.CustomEvent("Coin Pickup", new Dictionary<string, object>()
        {
            { "userID", AnalyticsSessionInfo.userId },
            { "value",  value }
        });
    }
}
