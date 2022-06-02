using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private void Start()
    {
        ToggleSound();

        SettingsWindow.toggleSound += ToggleSound;
    }

    private void OnDestroy()
    {
        SettingsWindow.toggleSound -= ToggleSound;
    }

    private void ToggleSound()
    {
        gameObject.SetActive(SaveLoadManager.settingsData.soundEnabled);
    }
}
