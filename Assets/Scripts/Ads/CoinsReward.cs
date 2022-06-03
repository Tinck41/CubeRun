using UnityEngine;

public class CoinsReward : MonoBehaviour, IReward
{
    [SerializeField] private int _value;

    public RewardType type { get; set; }

    private void Start()
    {
        type = RewardType.COIN;
    }

    public void AddReward()
    {
        AnalyticsHelper.OnAdWatch(type, _value);

        GameManager.instance.topHUD.AddDailyReward(_value);
    }
}
