public class GameOverState : GameState
{
    public override void EnterState()
    {
        UI?.SetActive(true);
    }

    public override void LeaveState()
    {
        UI?.SetActive(false);
    }

    public override void UpdateState()
    {

    }

    public void OnRestartButtonClick()
    {
        GameManager.instance.ReloadGame();
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameRunningState>());
    }

    public void OnHomeButtonClick()
    {
        GameManager.instance.ReloadGame();
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<MainMenuState>());
    }
}
