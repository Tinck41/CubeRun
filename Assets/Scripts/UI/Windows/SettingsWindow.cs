using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private ChangeIconButton _musicIcon;
    [SerializeField] private ChangeIconButton _soundsIcon;

    private void Start()
    {
        var soundsEnabled = PlayerDataHelper.isSoundsEnabled();
        var musicEnabled = PlayerDataHelper.isMusicEnabled();

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
        PlayerDataHelper.SetSoundsEnabled(!PlayerDataHelper.isSoundsEnabled());
    }

    public void OnMusicButtonClick()
    {
        PlayerDataHelper.SetMusicEnabled(!PlayerDataHelper.isMusicEnabled());
    }

    public void OnCreaditsButtonClick()
    {

    }
}
