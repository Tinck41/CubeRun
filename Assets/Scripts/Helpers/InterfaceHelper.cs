using UnityEngine;
using UnityEngine.UI;

public enum ScreenType
{
    Undefined,
    MainMenu,
    GameRunning,
    GameOver
}

public class InterfaceHelper : MonoBehaviour
{
    [SerializeField] public GameObject MainMenuScreen;
    [SerializeField] public GameObject GameRunningScreen;
    [SerializeField] public GameObject GameOverScreen;

    public GameObject GetScreen(ScreenType type)
    {
        switch(type)
        {
            case ScreenType.MainMenu: return MainMenuScreen;
            case ScreenType.GameRunning: return GameRunningScreen;
            case ScreenType.GameOver: return GameOverScreen;
            case ScreenType.Undefined: return null;
            default: return null;
        }
    }

}
