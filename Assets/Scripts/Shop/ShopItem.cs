using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private bool _isBought;

    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _buyButton;

    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _toggleSound;

    [SerializeField] private SkinType _skinType;

    public bool isSelected { get; private set; }

    private void Start()
    {
        _isBought = SaveLoadManager.playerData.avaliableSkins.Contains(_skinType);
        _itemNameText.text = LocaleHelper.GetString($"Skin.{_skinType}.Name");

        UpdateButton();
    }

    private bool CanBuy()
    {
        if (SaveLoadManager.playerData.coins >= _cost)
        {
            return true;
        }

        return false;
    }

    public void Buy()
    {
        _clickSound.Play();

        if (CanBuy())
        {
            SaveLoadManager.playerData.coins -= _cost;
            SaveLoadManager.playerData.avaliableSkins.Add(_skinType);

            GameManager.instance.topHUD.SetCoinsValue(SaveLoadManager.playerData.coins);

            _isBought = true;
            UpdateButton();

            AnalyticsHelper.OnSkinBought(_skinType);
        }
    }

    public void Equip()
    {
        _toggleSound.Play();

        GameManager.instance.GetPlayer().skinManager.SetSkin(_skinType);
        UpdateButton();

        AnalyticsHelper.OnSkinSet(_skinType);
    }

    private void Update()
    {
        if (_isBought)
        {
            if (SaveLoadManager.playerData.selectedSkin == _skinType)
            {
                _buyButton.interactable = false;
                _costText.text = LocaleHelper.GetString("UI.Shop.Item.Button.Equiped");
            }
            else
            {
                _buyButton.interactable = true;
                _costText.text = LocaleHelper.GetString("UI.Shop.Item.Button.Equip");
            }
        }
    }

    public void UpdateButton()
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
        }
        else
        {
            _buyButton.interactable = CanBuy();
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
