using UnityEngine;
using UnityEngine.Events;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private ChangeIconButton _musicIcon;
    [SerializeField] private ChangeIconButton _soundsIcon;

    public static event ToggleSound toggleSound;
    public delegate void ToggleSound();

    public static event ToggleMusic toggleMusic;
    public delegate void ToggleMusic();

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
        toggleSound();
    }

    public void OnMusicButtonClick()
    {
        SaveLoadManager.settingsData.musicEnabled = !SaveLoadManager.settingsData.musicEnabled;
        toggleMusic();
    }
}
