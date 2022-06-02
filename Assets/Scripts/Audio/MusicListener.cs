using UnityEngine;

public class MusicListener : MonoBehaviour
{
    private void Start()
    {
        ToggleMusic();

        SettingsWindow.toggleMusic += ToggleMusic;
    }

    private void OnDestroy()
    {
        SettingsWindow.toggleMusic -= ToggleMusic;
    }

    private void ToggleMusic()
    {
        gameObject.SetActive(SaveLoadManager.settingsData.musicEnabled);
    }
}
