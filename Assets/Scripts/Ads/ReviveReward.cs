using UnityEngine;

public class ReviveReward : MonoBehaviour, IReward
{
    public RewardType type { get; set; }

    private void Start()
    {
        type = RewardType.REVIVE;
    }

    public void AddReward()
    {
        AnalyticsHelper.OnAdWatch(type);

        GameManager.instance.GetChunkLoader().ReloadPlatformForRevive();
        GameManager.instance.SwitchState(GameManager.instance.GetComponent<GameRunningState>());
    }
}
