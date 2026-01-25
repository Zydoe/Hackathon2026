using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemRow : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private ItemData item;
    private int price;
    private ShopUI shop;

    public void Setup(ShopUI shopUI, ItemData itemData, int itemPrice)
    {
        shop = shopUI;
        item = itemData;
        price = itemPrice;

        icon.sprite = item.Sprite;
        nameText.text = item.ObjectName;
        priceText.text = price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(Buy);
    }

    void Buy()
    {
        shop.TryBuy(item, price);
    }
}
