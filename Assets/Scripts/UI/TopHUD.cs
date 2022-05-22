using UnityEngine;
using TMPro;
using System;

public class TopHUD : MonoBehaviour
{
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
}
