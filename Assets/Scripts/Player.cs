using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity
{
    private string playerName = "Johnny";
    private int level = 0;
    private int experience = 0;
    public AudioClip coinSound;
    public AudioClip damagedSound;
    private AudioSource audioSource;
    Transform hitbox;

    public static Player Instance;

    // Start is called before the first frame update
    void Awake()
    {
        hitbox = transform.Find("AttackHitbox");

        // If another Player already exists, delete this one (the scene copy)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(damagedSound);
        base.TakeDamage(damage);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetSpeed(1);
        SetStrength(1);
        SetMaxHp(10);
        SetHp(GetMaxHp());
    }
    public void Attack()
    {
        DontDestroyOnLoad(gameObject);

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
        audioSource.PlayOneShot(coinSound);
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
