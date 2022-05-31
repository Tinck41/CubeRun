using UnityEngine;
using System;
using System.Collections.Generic;

public enum SkinType
{
    UNDEFINED = 0,
    ORANGE,
    YELLOW
}

public class SkinManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _skins;

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
                skin.SetActive(false);
            }

            _skins[Convert.ToInt32(type) - 1].SetActive(true);

            SaveLoadManager.playerData.selectedSkin = type;
        }
    }
}
