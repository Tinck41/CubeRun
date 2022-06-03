using UnityEngine;
using System;

public enum SkinType
{
    UNDEFINED = 0,
    DEFAULT,
    SKILLET,
    FOUR_SQUARE
}

public class SkinManager : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;

    private void Start()
    {
        SetSkin(SaveLoadManager.playerData.selectedSkin);
    }

    public void SetSkin(SkinType type)
    {
        if (SaveLoadManager.playerData.avaliableSkins.Contains(type))
        {
            foreach(var skin in _skins)
            {
                skin.gameObject.SetActive(false);
            }

            _skins[Convert.ToInt32(type) - 1].gameObject.SetActive(true);

            SaveLoadManager.playerData.selectedSkin = type;

            AnalyticsHelper.OnSkinSet(type);
        }
        else
        {
            Debug.LogError($"Can't set skin of type {type}");

            foreach (var skin in _skins)
            {
                skin.gameObject.SetActive(false);
            }

            _skins[Convert.ToInt32(SkinType.DEFAULT) - 1].gameObject.SetActive(true);

            SaveLoadManager.playerData.selectedSkin = SkinType.DEFAULT;
        }
    }

    public Skin GetSelectedSkin()
    {
        return _skins[Convert.ToInt32(SaveLoadManager.playerData.selectedSkin) - 1];
    }

    public void Reset()
    {
        GetSelectedSkin().Reset();
    }
}
