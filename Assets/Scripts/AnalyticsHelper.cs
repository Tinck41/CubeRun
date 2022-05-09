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
            { "score", score },
            { "reason", reason.ToString() }
        });
    }
}
