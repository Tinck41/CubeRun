using UnityEngine;

public enum WindowType
{
    UNDEFINED = 0,
    SHOP_WINDOW,
    SETTING_WINDOW,
    GAME_OVER_WINDOW,
    PAUSE_WINDOW
}

public class WindowBase : MonoBehaviour
{
    [SerializeField] private WindowType _type;

    public void ShowWindow()
    {
        AnalyticsHelper.OnWindowOppened(_type);
    }
}
