using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    // [SerializeField] private HitPoints _hitPoints;
    public Image meterImage;
    public TextMeshProUGUI hpText;
    public Entity Character;
    void Start()
    {
        Character = GameObject.Find("Player").GetComponent<Player>();
        meterImage.fillAmount = 1;
        hpText.text = "10";
    }
    void Update()
    {
        meterImage.fillAmount = Character.GetHp() / Character.GetMaxHp();
        hpText.text = Character.GetHp().ToString("0");
    }
}
