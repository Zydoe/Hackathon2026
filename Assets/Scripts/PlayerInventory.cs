using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerInventory : MonoBehaviour{

    // void OnTriggerEnter2D(Collider2D collision) {
    //     if (collision.gameObject.CompareTag("PickUp")) {
    //         collision.gameObject.SetActive(false);
    //     }
    // }
    [SerializeField] Inventory _inventoryPrefab; 
    private Inventory _inventory;

    // void Start() {
    //     _hitPoints.Value = _startingHitPoints;
    //     _healthBar = Instantiate(_healthBarPrefab);
    //     _healthBar.Character = this;

    //     _inventory = Instantiate(_inventoryPrefab);
    // }
    // void OnTriggerEnter2D(Collider2D collision) {
    //     if (collision.gameObject.CompareTag("PickUp")){
    //         ItemData hitObject =
    //         collision.gameObject.GetComponent<Consumable>().Item;
    //         if (hitObject != null){
    //             print("Hit: " + hitObject.ObjectName);
    //             switch (hitObject.Type){
    //             case ItemData.ItemType.Coin:
    //                 break;
    //             case ItemData.ItemType.Health:
    //                 AdjustHitPoints(hitObject.Quantity);
    //                 break;
    //             }
    //             collision.gameObject.SetActive(false);
    //         }
    //     }
    // }
    // public void AdjustHitPoints(int amount) {
    //     _hitPoints = _hitPoints + amount;
    //     print("Adjusted hitpoints by: " + amount + ". New value: " + _hitPoints);
    // }

    // public override void ResetCharacter() {
    //     _inventory = Instantiate(_inventoryPrefab);
    //     _healthBar = Instantiate(_healthBarPrefab);
    //     _healthBar.Character = this;
    //     _hitPoints.Value = _startingHitPoints;
    // }
    // private void OnEnable() {
    //     ResetCharacter();
    // }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PickUp")) {
            ItemData hitObject =collision.gameObject.GetComponent<Consumable>().Item;
            if (hitObject != null) {
                print("Hit: " + hitObject.ObjectName);
                bool shouldDisappear = false;
                switch (hitObject.Type) {
                case ItemData.ItemType.Coin:
                    //shouldDisappear = true;
                    // shouldDisappear = _inventory.AddItem(hitObject);
                    break;
                case ItemData.ItemType.Health:
                    // shouldDisappear =
                    // AdjustHitPoints(hitObject.Quantity);
                    break;
                
                case ItemData.ItemType.Medkit:
                    // shouldDisappear =AdjustHitPoints(hitObject.Quantity);
                    break;
                case ItemData.ItemType.Key:
                    // shouldDisappear = _inventory.AddItem(hitObject);
                    break;
                case ItemData.ItemType.Fruit:
                    // shouldDisappear = _inventory.AddItem(hitObject);
                    break;
                }
                if (shouldDisappear)
                    collision.gameObject.SetActive(false);
            }
        }
    }
    // public bool AdjustHitPoints(int amount) {
    //     if (_hitPoints.Value < _maxHitPoints) {
    //         _hitPoints.Value = _hitPoints.Value + amount;
    //         print("Adjusted HP by: " + amount + ". New value: " +
    //         _hitPoints.Value);
    //         return true;
    //     }
    //     return false;
    // }


    // public override IEnumerator DamageCharacter(int damage, float interval) {
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
