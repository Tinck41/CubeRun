using UnityEngine;

public class MainMenuState : GameState
{
    [SerializeField] private GameObject _exitWindow;

    public override void EnterState()
    {
        GameManager.instance.topHUD.SetBarEnabled(BarType.COINS, true);
        GameManager.instance.topHUD.SetBarEnabled(BarType.RECORD, true);
        UI.SetActive(true);
    }

    public override void LeaveState()
    {
        UI.SetActive(false);
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _exitWindow.SetActive(true);
        }
    }

    public void OnPlayButtonClick()
    {
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameRunningState>());
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
