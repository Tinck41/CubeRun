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
    }

    public void SetRecordValue(int value)
    {
        _scoreText.text = "Best: " + value.ToString();
    }

    public void SetCoinsValue(int value)
    {
        _coinsText.text = value.ToString();
    }

    public void Reload()
    {
        var data = SaveLoadManager.playerData;

        _scoreText.text = "Best: " + data.record.ToString();
        _coinsText.text = data.coins.ToString();
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
