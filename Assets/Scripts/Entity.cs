using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Entity : MonoBehaviour
{
    [SerializeField] public float hp = 10;
    private float speed = 1;
    private int strength;
    private int stamina;
    [SerializeField] private int coins = 0;
    public float maxHp = 10;

    protected virtual void Update()
    {
        if (hp <= 0)
        {
            this.OnDeath();
        }
    }

    public float GetHp() => hp;

    public void SetHp(float value)
    {
        hp = value;
    }

    public int GetCoins() => coins;
    public void SetCoins(int value)
    {
        coins = value;
    }
    public float GetMaxHp() => maxHp;
    public void SetMaxHp(float value)
    {
        maxHp = value;
    }


    public float GetSpeed() => speed;

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public int GetStrength() => strength;

    public void SetStrength(int value)
    {
        strength = value;
    }

    public int GetStamina() => stamina;

    public void SetStamina(int value)
    {
        stamina = value;
    }
    public void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            OnDeath();
        }

    }

    //So Player can heal
    public bool AdjustHitPoints(int amount) {
        if (hp < maxHp) {
            hp += amount;
            return true;
        }
        return false;
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }


}