using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    public static EnemyRegistry Instance { get; private set; }

    public int AliveCount { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void RegisterEnemy()
    {
        AliveCount++;
    }

    public void UnregisterEnemy()
    {
        AliveCount = Mathf.Max(0, AliveCount - 1);
    }
}
