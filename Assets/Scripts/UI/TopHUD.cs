using UnityEngine;
using TMPro;

public enum BarType
{
    COINS,
    RECORD
}

public class TopHUD : MonoBehaviour
{
    [SerializeField] private GameObject _recordBar;
    [SerializeField] private GameObject _coinsBar;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    public void Start()
    {
        Reload();

        PlayerDataHelper.CoinsChanged += OnCoinsChanged;
        PlayerDataHelper.RecordChanged += OnRecordChanged;
    }

    private void OnRecordChanged(int value)
    {
        _scoreText.text = "Best: " + value.ToString();
    }

    private void OnCoinsChanged(int value)
    {
        _coinsText.text = PlayerDataHelper.GetCoins().ToString();
    }

    public void Reload()
    {
        _scoreText.text = "Best: " + PlayerDataHelper.GetRecord().ToString();
        _coinsText.text = PlayerDataHelper.GetCoins().ToString();
    }

    public void SetBarEnabled(BarType bar, bool value)
    {
        switch(bar)
        {
            case BarType.COINS:
                {
                    if (_coinsBar != null)
                    {
                        _coinsBar.gameObject.SetActive(value);
                    }
                    break;
                }
            case BarType.RECORD:
                {
                    if (_recordBar != null)
                    {
                        _recordBar.gameObject.SetActive(value);
                    }
                    break;
                }
            default:
                break;
        }
    }
}
