using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    private const int _numSlots = 5;
    private Image[] _itemImages = new Image[_numSlots]; 
    [SerializeField] public ItemData[] _items = new ItemData[_numSlots]; 
    public Color32 selectedColor = new Color32(255, 255, 255, 255); // white
    public Color32 unselectedColor = new Color32(140, 140, 140, 255); // grey
    public int selectedIndex = 0;

    private GameObject[] _slots = new GameObject[_numSlots];
    public void Start() {
        CreateSlots();
    }
    public void CreateSlots() {
        if (_slotPrefab != null) {
            for (int i = 0; i < _numSlots; i++) {
                GameObject newSlot = Instantiate(_slotPrefab);
                newSlot.name = "ItemSlot_" + i;
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                _slots[i] = newSlot;
                _itemImages[i] =
                newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }
    public bool AddItem(ItemData itemToAdd) {
        for (int i = 0; i < _items.Length; i++) {
            if (_items[i] != null && _items[i].Type == itemToAdd.Type &&
            itemToAdd.IsStackable == true) {
                _items[i].Quantity = _items[i].Quantity + 1;
                Slot slotScript = _slots[i].gameObject.GetComponent<Slot>();
                TextMeshProUGUI quantityText = slotScript.QtyText;
                quantityText.enabled = true;
                quantityText.text = _items[i].Quantity.ToString();
                return true;
            }
            if (_items[i] == null) {
                _items[i] = Instantiate(itemToAdd);
                _items[i].Quantity = 1;
                _itemImages[i].sprite = itemToAdd.Sprite;
                _itemImages[i].enabled = true;
                Slot slotScript = _slots[i].gameObject.GetComponent<Slot>();
                TextMeshProUGUI quantityText = slotScript.QtyText;
                quantityText.enabled = true; quantityText.text =
                _items[i].Quantity.ToString();
                return true;
            }
        }
        return false;
    }
    
    public bool RemoveItem(ItemData itemToRemove)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            // Check if the slot has the item we're trying to remove
            if (_items[i] != null && _items[i].Type == itemToRemove.Type)
            {
                _items[i].Quantity--;

                Slot slotScript = _slots[i].GetComponent<Slot>();
                TextMeshProUGUI quantityText = slotScript.QtyText;

                if (_items[i].Quantity <= 0)
                {
                    // Remove item completely
                    _items[i] = null;
                    _itemImages[i].sprite = null;
                    _itemImages[i].enabled = false;
                    quantityText.enabled = false;
                }
                else
                {
                    // Update quantity display
                    quantityText.text = _items[i].Quantity.ToString();
                }

                SetSelectedSlot(selectedIndex);
                return true;
            }
        }
        return false;
    }

    public void SetSelectedSlot(int index)
    {
        selectedIndex = index;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i == index)
                _slots[i].GetComponentInChildren<Image>().color = selectedColor;
            else
                _slots[i].GetComponentInChildren<Image>().color = unselectedColor;
        }

    }
    //Check if player has the item
    public bool HasItem(ItemData.ItemType item)
    {
        foreach (ItemData i in _items)
        {
            if (i == null) continue;
            if (i.Type == item) return true;
        }
        return false;

    }
}
