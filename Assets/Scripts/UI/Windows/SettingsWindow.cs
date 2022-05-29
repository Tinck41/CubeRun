using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private ChangeIconButton _musicIcon;
    [SerializeField] private ChangeIconButton _soundsIcon;

    private void Start()
    {
        var soundsEnabled = SaveLoadManager.settingsData.soundEnabled;
        var musicEnabled = SaveLoadManager.settingsData.musicEnabled;
        
        if (!soundsEnabled)
        {
            _soundsIcon?.Swap();
        }

        if (!musicEnabled)
        {
            _musicIcon?.Swap();
        }
    }

    public void OnSoundsButtonClick()
    {
        SaveLoadManager.settingsData.soundEnabled = !SaveLoadManager.settingsData.soundEnabled;
    }

    public void OnMusicButtonClick()
    {
        SaveLoadManager.settingsData.musicEnabled = !SaveLoadManager.settingsData.musicEnabled;
    }
}
