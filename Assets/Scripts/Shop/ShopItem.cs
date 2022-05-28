using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private bool _isBought;

    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _buyButton;

    [SerializeField] private SkinType _skinType;

    public bool isSelected { get; private set; }

    private void Start()
    {
        UpdateButton();
    }

    private bool CanBuy()
    {
        if (PlayerDataHelper.GetCoins() > _cost)
        {
            return true;
        }

        return false;
    }

    public void Buy()
    {
        if (CanBuy())
        {
            PlayerDataHelper.AddCoins(-_cost);
            _isBought = true;
            UpdateButton();
        }
    }

    public void Equip()
    {
        GameManager.instance.GetPlayer().SetSkin(_skinType);
    }

    private void UpdateButton()
    {
        if (_buyButton == null)
        {
            Debug.LogError("buy button is null!");
            return;
        }

        if (_isBought)
        {
            _buyButton.onClick.RemoveListener(Buy);
            _buyButton.onClick.AddListener(Equip);
            _costText.text = "Equip";
        }
        else
        {
            _buyButton.onClick.RemoveListener(Equip);
            _buyButton.onClick.AddListener(Buy);
            _costText.text = _cost.ToString();
        }
    }

    public void SetSelected(bool value)
    {
        isSelected = value;
    }

    public SkinType GetSkinType()
    {
        return _skinType;
    }
}
