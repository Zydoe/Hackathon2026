using System;
using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public event Action EnemyDied;
    protected AudioSource audioSource;
    public AudioClip detectSound;
    public AudioClip deathSound;
    public AudioClip damagedSound;
    public GameObject coinPrefab;
    public float minExplosionForce = 0.1f;
    public float maxExplosionForce = 1f;
    private bool canAttack = true;



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public virtual void Attack(Entity target)
    {
        if (!canAttack) return;
        target.TakeDamage(GetStrength());
        StartCoroutine(AttackCooldown(1.0f)); // 1 second cooldown
    }
    IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
    public override void OnDeath()
    {
        if (EnemyRegistry.Instance != null)
            EnemyRegistry.Instance.UnregisterEnemy();

        // Create a temporary audio object
        if (deathSound != null)
        {
            GameObject audioObj = new GameObject("DeathSound");
            AudioSource tempSource = audioObj.AddComponent<AudioSource>();
            tempSource.clip = deathSound;
            tempSource.Play();

            // Destroy the audio object after the clip finishes
            Destroy(audioObj, deathSound.length);
        }

        DropCoins();
        base.OnDeath();
    }

    public virtual void PlayDetectSound()
    {
        audioSource.PlayOneShot(detectSound);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        audioSource.PlayOneShot(damagedSound);
    }

    public void DropCoins()
    {
        for (int i = 0; i < GetCoins(); i++)
        {
            // Spawn coin
            GameObject coin = Instantiate(
                coinPrefab,
                transform.position,
                Quaternion.identity
            );

            // Give it a random explosion direction
            Vector2 dir = UnityEngine.Random.insideUnitCircle;
            float force = UnityEngine.Random.Range(minExplosionForce, maxExplosionForce);


            // Apply force
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * force, ForceMode2D.Impulse);
        }
    }
}
