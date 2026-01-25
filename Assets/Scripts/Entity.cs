using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Entity : MonoBehaviour
{
    [SerializeField] private int hp = 10;
    private float speed = 1;
    private int strength;
    private int stamina;
    [SerializeField] private int coins = 0;
    private int maxHp = 10;

    protected virtual void Update()
    {
        if (hp <= 0)
        {
            this.OnDeath();
        }
    }

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
    public virtual void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            OnDeath();
        }

    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }


}