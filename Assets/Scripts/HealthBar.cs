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
    [SerializeField]private Entity _character;
    public Entity Character {
        get {return _character;}
        set {_character = value;}
    }
    void Start()
    {
        if (_character == null && Player.Instance != null)
        _character = Player.Instance;
    }
    void Update() {
        if (_character != null) {
            _meterImage.fillAmount = _character.hp / _character.maxHp;
            _hpText.text = "HP:" + (_meterImage.fillAmount * 100);
        }
    }
}
