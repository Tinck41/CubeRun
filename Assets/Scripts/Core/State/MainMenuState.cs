using UnityEngine;

public class MainMenuState : GameState
{
    [SerializeField] private GameObject _settingsWindow;

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

    public void OnPlayButtonClick()
    {
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameRunningState>());
    }
}
