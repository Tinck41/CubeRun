using System.Collections;
using UnityEngine;

public class GamePausedState : GameState
{
    [SerializeField] private GameObject pauseWindow;
    public override void EnterState()
    {
        UI?.SetActive(true);
        pauseWindow?.SetActive(true);
        Time.timeScale = 0;
    }

    public override void LeaveState()
    {
        UI?.SetActive(false);
        pauseWindow?.SetActive(false);
        Time.timeScale = 1;
    }

    public override void UpdateState()
    {
        
    }

    public void OnResumeButtonClick()
    {
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameRunningState>());
    }

    public void OnHomeButtonClick()
    {
        GameManager.instance.ReloadGame();
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<MainMenuState>());
    }
}
