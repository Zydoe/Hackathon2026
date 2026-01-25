using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private string playerName = "Johnny";
    private int level = 0;
    private int experience = 0;
    Transform hitbox;
    // Start is called before the first frame update
    void Awake()
    {
        hitbox = transform.Find("AttackHitbox");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void Start()
    {
        SetSpeed(1);
        SetStrength(1);
        SetMaxHp(10);
        SetHp(GetMaxHp());
    }
    public void Attack()
    {
        hitbox.GetComponent<Collider2D>().enabled = true;
        // Implement attack logic here
        StartCoroutine(DisableHitboxAfterDelay(0.2f));
    }
    public int GetAttackDamage()
    {
        return GetStrength();
    }

    IEnumerator DisableHitboxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hitbox.GetComponent<Collider2D>().enabled = false;
    }
    //getters and setters
    public string GetPlayerName() => playerName;

    public void SetPlayerName(string value)
    {
        playerName = value;
    }

    public void AddCoins(int amount)
    {
        SetCoins(GetCoins() + amount);
    }
    public int GetLevel() => level;

    public void SetLevel(int value)
    {
        level = value;
    }

    public int GetExperience() => experience;

    public void SetExperience(int value)
    {
        experience = value;
    }
}
