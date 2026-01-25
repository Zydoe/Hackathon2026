using System;
using UnityEngine;

public class Enemy : Entity
{
    public event Action EnemyDied;

    public GameObject coinPrefab;
    public float minExplosionForce = 0.05f;
    public float maxExplosionForce = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    public override void OnDeath()
    {
        EnemyDied?.Invoke(); //For the wave management to check if enemy died.
        DropCoins();
        Destroy(gameObject);
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
