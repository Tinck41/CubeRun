using UnityEngine;
using TMPro;
using DG.Tweening;

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
    [SerializeField] private TextMeshProUGUI _rewardText;

    [SerializeField] private Transform _rewardTextStartPosition;
    [SerializeField] private Transform _rewardTextEndPosition;

    [SerializeField] private float _rewardShowTime;

    public void Start()
    {
        Reload();
    }

    public void SetRecordValue(int value)
    {
        _scoreText.text = LocaleHelper.GetString("UI.TopHud.Record") + value.ToString();
    }

    public void SetCoinsValue(int value)
    {
        _coinsText.text = value.ToString();
    }

    public void Reload()
    {
        var data = SaveLoadManager.playerData;

        _scoreText.text = LocaleHelper.GetString("UI.TopHud.Record") + data.record.ToString();
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

    public void AddDailyReward(int value)
    {
        SaveLoadManager.playerData.coins += value;
        SetCoinsValue(SaveLoadManager.playerData.coins);

        _rewardText.gameObject.transform.position = _rewardTextStartPosition.position;
        _rewardText.gameObject.SetActive(true);

        _rewardText.text = "+" + value.ToString();
        
        _rewardText.gameObject.transform.DOMoveY(_rewardTextEndPosition.position.y, _rewardShowTime)
            .OnComplete(() => _rewardText.gameObject.SetActive(false));

        Sequence sequence = DOTween.Sequence(_rewardText);
        sequence.Append(_rewardText.transform.DOMoveY(_rewardTextEndPosition.position.y, _rewardShowTime));
        sequence.Join(_rewardText.DOFade(0, _rewardShowTime));
        sequence.OnComplete(() => {
            _rewardText.gameObject.SetActive(false);
            _rewardText.color = Color.black;
        });
    }
}
