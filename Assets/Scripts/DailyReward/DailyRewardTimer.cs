using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardTimer : MonoBehaviour
{
    [SerializeField] private float _msToWait;
    [SerializeField] private int _rewardValue;
    [SerializeField] private Button _claimButton;
    [SerializeField] private Image _taimerImage;

    private ulong _lastTimeClicked;

    private void Start()
    {

        ulong.TryParse(PlayerPrefs.GetString("LastTimeClicked"), out _lastTimeClicked);

        if (!Ready())
        {
            _claimButton.interactable = false;
            _taimerImage.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!_claimButton.IsInteractable())
        {
            if (Ready())
            {
                _claimButton.interactable = true;
                _taimerImage.gameObject.SetActive(false);
                return;
            }
            ulong diff = ((ulong)DateTime.UtcNow.Ticks - _lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(_msToWait - m) / 1000.0f;

            _taimerImage.fillAmount = (_msToWait - m) / _msToWait;
        }
    }


    public void Claim()
    {
        AnalyticsHelper.OnDailyRewardGet(_rewardValue);

        _lastTimeClicked = (ulong)DateTime.UtcNow.Ticks;
        PlayerPrefs.SetString("LastTimeClicked", _lastTimeClicked.ToString());

        _claimButton.interactable = false;
        _taimerImage.gameObject.SetActive(true);

        GameManager.instance.topHUD.AddDailyReward(_rewardValue);

        NotificationManager.instance.RegisterRewardReadyNotification(_msToWait);
    }

    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.UtcNow.Ticks - _lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(_msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            //Send push notification
            return true;
        }

        return false;
    }
}



