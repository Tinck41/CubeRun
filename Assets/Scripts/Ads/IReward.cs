public interface IReward 
{
    public RewardType type { get; set; }

    public void AddReward();
}

public enum RewardType
{
    UNDEFINED = 0,
    COIN,
    REVIVE
}