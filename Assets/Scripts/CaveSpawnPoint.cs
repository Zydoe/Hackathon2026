using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpawnPoint : MonoBehaviour
{
    public float minPlayerDistance = 6f;

    public bool CanSpawn(Transform player)
    {
        if (player == null) return true;
        return Vector2.Distance(transform.position, player.position) >= minPlayerDistance;
    }
}
