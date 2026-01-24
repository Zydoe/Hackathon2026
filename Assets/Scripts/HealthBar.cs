using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    // [SerializeField] private HitPoints _hitPoints;
    [SerializeField] private Image _meterImage; [SerializeField]
    private TextMeshProUGUI _hpText; 
    [SerializeField]private PlayerInventory _character;
    public PlayerInventory Character {
        get {return _character;}
        set {_character = value;}
    }
    void Start()
    {
        
    }
    void Update() {
        if (_character != null) {
            _meterImage.fillAmount = _character.currentHealth / _character.maxHealth;
            _hpText.text = "HP:" + (_meterImage.fillAmount * 100);
        }
    }
}
