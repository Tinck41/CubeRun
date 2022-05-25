using UnityEngine;
using System;

public static class PlayerDataHelper
{
    public static Action<int> CoinsChanged;
    public static Action<int> RecordChanged;
    public static Action<bool> MusicEnabled;
    public static Action<bool> SoundsEnabled;

    private static int _score = 0;

    public static void AddCoins(int value)
    {
        PlayerPrefs.SetInt("Coins", GetCoins() + value);
        CoinsChanged?.Invoke(GetCoins());
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public static void SetRecord(int value)
    {
        PlayerPrefs.SetInt("HighestScore", value);
        RecordChanged?.Invoke(value);
    }

    public static int GetRecord()
    {
        return PlayerPrefs.GetInt("HighestScore", 0);
    }

    public static void SetScore(int value)
    {
        _score = value;
    }

    public static void AddScore(int value)
    {
        _score += value;

        if (_score > GetRecord())
        {
            SetRecord(_score);
        }
    }

    public static int GetScore()
    {
        return _score;
    }

    public static void SetMusicEnabled(bool value)
    {
        PlayerPrefs.SetInt("MusicEnabled", Convert.ToInt32(value));
        MusicEnabled?.Invoke(value);
    }

    public static bool isMusicEnabled()
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt("MusicEnabled", 1));
    }

    public static void SetSoundsEnabled(bool value)
    {
        PlayerPrefs.SetInt("SoundsEnabled", Convert.ToInt32(value));
        SoundsEnabled?.Invoke(value);
    }

    public static bool isSoundsEnabled()
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt("SoundsEnabled", 1));
    }
}
