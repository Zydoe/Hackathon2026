using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttackHitbox : MonoBehaviour
{
    Player player;
    void Start()
    {
        // Initially disable the hitbox collider
        GetComponent<Collider2D>().enabled = false;
        player = GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(player.GetAttackDamage()); // Example damage value
        }
    }
}