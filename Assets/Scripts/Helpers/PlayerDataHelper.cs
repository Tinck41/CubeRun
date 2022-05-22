using UnityEngine;
using System;

public static class PlayerDataHelper
{
    public static Action<int> CoinsChanged;
    public static Action<int> RecordChanged;

    public static void AddCoins(int value)
    {
        PlayerPrefs.SetInt("Coins", GetCoins() + value);
        CoinsChanged?.Invoke(value);
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public static void SetRecord(int value)
    {
        PlayerPrefs.SetInt("HighestScore", GetRecord() + value);
        RecordChanged?.Invoke(value);
    }

    public static int GetRecord()
    {
        return PlayerPrefs.GetInt("HighestScore", 0);
    }
}
