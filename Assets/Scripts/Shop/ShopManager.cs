using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _items;
    [SerializeField] private SelectedItem _selectedItemHolder;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _selectedItemHolder.SetSkin(GameManager.instance.GetPlayer().GetSkin());
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
                _selectedItemHolder.SetSkin(item.GetSkinType());
            }
        }
    }
}
