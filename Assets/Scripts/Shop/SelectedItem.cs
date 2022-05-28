using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skins;

    public void SetSkin(SkinType type)
    {
        if (type == SkinType.UNDEFINED)
        {
            Debug.LogError("skin type is undefined!");
            return;
        }

        HideAllSkins();

        _skins[Convert.ToInt32(type) - 1].SetActive(true);
    }

    private void HideAllSkins()
    {
        foreach(var skin in _skins)
        {
            skin.gameObject.SetActive(false);
        }
    }
}
