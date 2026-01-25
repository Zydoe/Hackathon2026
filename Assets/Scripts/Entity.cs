using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Entity : MonoBehaviour
{
    private int hp;
    private float speed = 1;
    private int strength;
    private int stamina;
    private int coins = 0;
    private int maxHp = 10;



    public int GetHp() => hp;

    public void SetHp(int value)
    {
        hp = value;
    }

    public int GetCoins() => coins;
    public void SetCoins(int value)
    {
        coins = value;
    }
    public int GetMaxHp() => maxHp;
    public void SetMaxHp(int value)
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

    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}