using UnityEngine;

public class CoinsReward : MonoBehaviour, IReward
{
    [SerializeField] private int _value;

    public void AddReward()
    {
        //SaveLoadManager.playerData.coins += _value;

        //GameManager.instance.topHUD.SetCoinsValue(SaveLoadManager.playerData.coins);

        GameManager.instance.topHUD.AddDailyReward(_value);
    }
}
