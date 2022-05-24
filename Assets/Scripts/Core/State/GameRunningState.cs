using UnityEngine;

public class GameRunningState : GameState
{
    [SerializeField] private Player _player;

    public void Start()
    {
        Player.PlayerDead += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        Player.PlayerDead -= OnPlayerDeath;
    }

    public override void EnterState()
    {
        UI?.SetActive(true);
        GameManager.instance.inputDetection.AllowDetection(true);
    }

    public override void LeaveState()
    {
        UI?.SetActive(false);
        GameManager.instance.inputDetection.AllowDetection(false);
    }

    public override void UpdateState()
    {

    }

    private void OnPlayerDeath()
    {
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameOverState>());
    }

    private void OnApplicationPause(bool pause)
    {
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GamePausedState>());
    }
}
