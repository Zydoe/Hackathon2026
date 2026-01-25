using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CaveSpawnPoint : MonoBehaviour
{
    [Header("Optional")]
    public float minPlayerDistance = 6f; // don’t spawn right on top of player

    public bool CanSpawn(Transform player)
    {
        if (player == null) return true;
        return Vector2.Distance(transform.position, player.position) >= minPlayerDistance;
    }
}
