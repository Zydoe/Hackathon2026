using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Diagnostics;

public class PlayerInventory : MonoBehaviour{


    public float currentHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] private float bombVelocity = 2;
    [SerializeField] private HealthBar _healthBarPrefab; private
    HealthBar _healthBar;
    [SerializeField] Inventory _inventoryPrefab; 
    private Inventory _inventory;
    

    void Start() {
        currentHealth = maxHealth;
        _healthBar = Instantiate(_healthBarPrefab);
        _healthBar.Character = this;

        _inventory = Instantiate(_inventoryPrefab);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _inventory.SetSelectedSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) _inventory.SetSelectedSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) _inventory.SetSelectedSlot(2);
        if (Input.GetMouseButtonDown(0))
        {
            print("USE ITEM");
            UseItem(_inventory.selectedIndex);
        }
    }


    void UseItem(int index)
    {
        ItemData item = _inventory._items[_inventory.selectedIndex];
        if (item != null)
        {
            print("Used: " + item.ObjectName);
            bool shouldDisappear = false;
            switch (item.Type)
            {
                case ItemData.ItemType.Health:

                    shouldDisappear = AdjustHitPoints(2);
                    break;
                case ItemData.ItemType.Bomb:

                    shouldDisappear = FireBomb(item);
                    break;

            }
            if (shouldDisappear)
            {

                _inventory.RemoveItem(item);
            }


        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PickUp")) {
            ItemData hitObject =collision.gameObject.GetComponent<Consumable>().Item;
            if (hitObject != null) {
                _inventory.AddItem(hitObject);
                collision.gameObject.SetActive(false);

            }
        }
    }
    

    public bool AdjustHitPoints(int amount) {
        if (currentHealth < maxHealth) {
            currentHealth = currentHealth + amount;
            print("Adjusted HP by: " + amount + ". New value: " +
            currentHealth);
            return true;
        }
        return false;
    }
    public bool FireBomb(ItemData item)
    {
        if (item == null || item.Prefab == null)
        {
            print("Bomb item or prefab is missing!");
            return false;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject bomb = Instantiate(item.Prefab, transform.position, Quaternion.identity); 
        
        if (bomb != null)
        {
            Arc arcScript = bomb.GetComponent<Arc>();
            float travelDuration = 1.0f / bombVelocity;
            StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
            return true;
        }
        return false;
    }


    // public IEnumerator DamageCharacter(int damage, float interval) {
    //     while (true) {
    //         _hitPoints.Value = _hitPoints.Value - damage;
    //         if (_hitPoints.Value <= float.Epsilon) {
    //             KillCharacter();
    //             break;
    //         }
    //         if (interval > float.Epsilon) {
    //             yield return new WaitForSeconds(interval);
    //         }
    //         else {
    //             break;
    //         }
    //     }
    // }
    // public override void KillCharacter() {
    //     base.KillCharacter();
    //     Destroy(_healthBar.gameObject);
    //     Destroy(_inventory.gameObject);
    // }

}
