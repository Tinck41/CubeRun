using UnityEngine;
using System.Collections;

public class GameOverState : GameState
{
    [SerializeField] private RewardedAdsButton watchAdButton;

    public override void EnterState()
    {
        StartCoroutine(Show());
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

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(1);

        UI?.SetActive(true);
        UI.GetComponent<GameOverScreen>().Show();

        yield return null;
    }
}
