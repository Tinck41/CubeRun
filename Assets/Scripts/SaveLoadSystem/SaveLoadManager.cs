using UnityEngine;
using System.Collections.Generic;

public static class SaveLoadManager
{
    private enum DataType
    {
        PLAYER,
        SETTINGS
    }

    private static Dictionary<DataType, string> DataTypeConverter = new Dictionary<DataType, string>()
    {
        {DataType.PLAYER, "player" },
        {DataType.SETTINGS, "setting" },
    };

    public static GameData.PlayerData playerData { get; private set; }
    public static GameData.SettingsData settingsData{ get; private set; }

    public static void Save()
    {
        PlayerPrefs.SetString(DataTypeConverter[DataType.PLAYER], JsonUtility.ToJson(playerData));
        PlayerPrefs.SetString(DataTypeConverter[DataType.SETTINGS], JsonUtility.ToJson(settingsData));
    }

    public static void Load()
    {
        playerData = new GameData.PlayerData();
        settingsData = new GameData.SettingsData();

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(DataTypeConverter[DataType.PLAYER]), playerData);
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(DataTypeConverter[DataType.SETTINGS]), settingsData);
    }
}
