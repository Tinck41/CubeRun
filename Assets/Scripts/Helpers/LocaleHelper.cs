using UnityEngine.Localization.Settings;

public static class LocaleHelper
{
    public static string GetString(string key)
    {
        var str = LocalizationSettings.StringDatabase.GetLocalizedString("Text Locale", key);
        if (str.Contains("No translation found for"))
        {
            return key;
        }

        return str;
    }
}
