using System;
using System.Collections;
using UnityEngine;

public class Enemy : Entity
{


    public GameObject coinPrefab;
    public float minExplosionForce = 0.05f;
    public float maxExplosionForce = 0.5f;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {

    }

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

        DropCoins();
        base.OnDeath();
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
