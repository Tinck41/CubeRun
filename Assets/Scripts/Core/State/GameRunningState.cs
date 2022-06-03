using UnityEngine;

public class GameRunningState : GameState
{
    [SerializeField] private Player _player;

    private bool _appPaused;

    public void Start()
    {
        _appPaused = false;

        Player.PlayerDead += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        Player.PlayerDead -= OnPlayerDeath;
    }

    public override void EnterState()
    {
        UI.SetActive(true);
        GameManager.instance.inputDetection.AllowDetection(true);
        GameManager.instance.topHUD.SetBarEnabled(BarType.COINS, true);
        GameManager.instance.topHUD.SetBarEnabled(BarType.RECORD, true);
    }

    public override void LeaveState()
    {
        UI.SetActive(false);
        GameManager.instance.inputDetection.AllowDetection(false);
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || _appPaused)
        {
            GameManager.instance.SwitchState(GameManager.instance.GetComponent<GamePausedState>());
        }
    }

    private void OnPlayerDeath()
    {
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameOverState>());
    }

    private void OnApplicationPause(bool pause)
    {
        _appPaused = pause;
    }
}
