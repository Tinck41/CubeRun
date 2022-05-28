using UnityEngine;

public class GameOverState : GameState
{
    [SerializeField] private RewardedAdsButton watchAdButton;

    public override void EnterState()
    {
        UI?.SetActive(true);
        if (!watchAdButton.loaded)
        {
            watchAdButton?.LoadAd();
        }
        GameManager.instance.topHUD.SetBarEnabled(BarType.COINS, false);
        GameManager.instance.topHUD.SetBarEnabled(BarType.RECORD, false);
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
