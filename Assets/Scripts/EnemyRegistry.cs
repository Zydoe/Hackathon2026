using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    public static EnemyRegistry Instance { get; private set; }

    [SerializeField] private int aliveCount;

    public int AliveCount => aliveCount; // read-only access

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterEnemy()
    {
        aliveCount++;
    }

    public void UnregisterEnemy()
    {
        aliveCount = Mathf.Max(0, aliveCount - 1);
    }
}
