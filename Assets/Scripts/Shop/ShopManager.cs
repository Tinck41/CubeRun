using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _items;
    [SerializeField] private SelectedItem _selectedItemHolder;
    
    [SerializeField] private TextMeshProUGUI _headerText;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        var skinType = SaveLoadManager.playerData.selectedSkin;
        _selectedItemHolder.SetSkin(skinType);
        _headerText.text = LocaleHelper.GetString($"Skin.{skinType}.Name");
        UpdateBuyButtons();
    }

    public void UnselectAll()
    {
        if (_items.Count < 1)
        {
            return;
        }

        foreach (var item in _items)
        {
            item.SetSelected(false);
        }
    }

    public void UpdateSelectedItem()
    {
        if (_items.Count < 1)
        {
            return;
        }

        foreach(var item in _items)
        {
            if (item.isSelected)
            {
                var skinType = item.GetSkinType();
                _selectedItemHolder.SetSkin(skinType);
                _headerText.text = LocaleHelper.GetString($"Skin.{skinType}.Name");
            }
        }
    }

    private void UpdateBuyButtons()
    {
        if (_items.Count < 1)
        {
            return;
        }

        foreach (var item in _items)
        {
            item.UpdateButton();
        }
    }
}
