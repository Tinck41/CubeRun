using UnityEngine;

public class CoinsReward : MonoBehaviour, IReward
{
    public void AddReward()
    {
        PlayerDataHelper.AddCoins(100);
    }
}
