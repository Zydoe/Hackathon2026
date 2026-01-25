using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopUI : MonoBehaviour
{
    [Serializable]
    public class ShopOffer
    {
        public ItemData item;
        public int price;
    }

    [Header("Items sold in shop")]
    [SerializeField] private List<ShopOffer> offers = new List<ShopOffer>();

    [Header("UI")]
    [SerializeField] private Transform contentRoot;
    [SerializeField] private ShopItemRow itemRowPrefab;

    [Header("Player Inventory Reference")]
    [SerializeField] private PlayerInventory playerInventory;

    private Inventory inventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        inventory = FindObjectOfType<Inventory>(); 
        BuildShop();
    }


    void BuildShop()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();

        // Clear old items
        for (int i = contentRoot.childCount - 1; i >= 0; i--)
            Destroy(contentRoot.GetChild(i).gameObject);

        // Create UI rows
        foreach (var offer in offers)
        {
            if (offer.item == null) continue;

            ShopItemRow row = Instantiate(itemRowPrefab, contentRoot);
            row.Setup(this, offer.item, offer.price);
        }
    }

    public void TryBuy(ItemData item, int price)
    {
        if (Player.Instance == null || inventory == null) return;

        int coins = Player.Instance.GetCoins();

        if (coins < price)
        {
            Debug.Log("Not enough coins!");
            return;
        }

        // Try add item to inventory
        bool added = inventory.AddItem(item);

        if (!added)
        {
            Debug.Log("Inventory full!");
            return;
        }

        // Remove coins only if item was added
        Player.Instance.SetCoins(coins - price);

        Debug.Log("Bought: " + item.ObjectName);
    }

    void OnEnable()
    {
        if (playerInventory != null)
            playerInventory.EnableInventoryInput(false);

        BuildShop();
    }
    void OnDisable()
    {
        if (playerInventory != null)
            playerInventory.EnableInventoryInput(true);
    }

}
