using UnityEngine;
using TMPro;

public class TopHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    public void Start()
    {
        Reload();
    }

    public void Reload()
    {
        _scoreText.text = "Best: " + PlayerDataHelper.GetRecord().ToString();
        _coinsText.text = PlayerDataHelper.GetCoins().ToString();
    }
}
