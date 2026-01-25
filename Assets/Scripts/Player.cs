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
    void Update()
    {

    }

    void Start()
    {
        SetSpeed(1);
    }
    public void Attack()
    {
        hitbox.GetComponent<Collider2D>().enabled = true;
        // Implement attack logic here
        Debug.Log("Player attacked with strength: " + GetStrength());
    }
    public int GetAttackDamage()
    {
        return GetStrength();
    }


    //getters and setters
    public string GetPlayerName() => playerName;

    public void SetPlayerName(string value)
    {
        playerName = value;
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
