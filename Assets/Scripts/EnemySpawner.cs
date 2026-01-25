using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyEntry
{
    public GameObject prefab;
    [Range(0f, 1f)] public float weight = 1f;
    public bool isRanged;
}

public class EnemySpawnerFromCaves : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player;
    [SerializeField] private List<CaveSpawnPoint> caves = new List<CaveSpawnPoint>();

    [Header("Enemies (weighted)")]
    [SerializeField] private List<EnemyEntry> enemies = new List<EnemyEntry>();

    [Header("Night Settings")]
    [SerializeField] private float nightDuration = 120f;
    [SerializeField] private float startSpawnInterval = 1.2f;
    [SerializeField] private float endSpawnInterval = 0.35f;

    [Header("Caps")]
    [SerializeField] private int maxAlive = 25;
    [SerializeField] private int maxRangedAlive = 4;

    private float nightTimer;
    private int alive;
    private int rangedAlive;
    private bool running;

    public void StartNight()
    {
        if (running) return;
        running = true;
        nightTimer = 0f;
        StartCoroutine(SpawnLoop());
    }

    public void StopNight()
    {
        running = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnLoop()
    {
        while (running && nightTimer < nightDuration)
        {
            nightTimer += Time.deltaTime;

            // Ramp spawn interval over the night
            float t = Mathf.Clamp01(nightTimer / nightDuration);
            float interval = Mathf.Lerp(startSpawnInterval, endSpawnInterval, t);

            // Only spawn if we have room
            if (alive < maxAlive)
            {
                CaveSpawnPoint cave = PickCave();
                if (cave != null)
                {
                    EnemyEntry entry = PickEnemy();
                    if (entry != null)
                    {
                        // Enforce ranged cap
                        if (!entry.isRanged || rangedAlive < maxRangedAlive)
                            Spawn(entry, cave.transform.position);
                    }
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }

    private CaveSpawnPoint PickCave()
    {
        if (caves.Count == 0) return null;

        // Try a few times to find a cave far enough from player
        for (int i = 0; i < 8; i++)
        {
            var c = caves[Random.Range(0, caves.Count)];
            if (c != null && c.CanSpawn(player)) return c;
        }

        // If none qualify, fallback to any cave
        return caves[Random.Range(0, caves.Count)];
    }

    private EnemyEntry PickEnemy()
    {
        if (enemies.Count == 0) return null;

        float total = 0f;
        foreach (var e in enemies) total += Mathf.Max(0f, e.weight);
        if (total <= 0f) return enemies[0];

        float r = Random.value * total;
        float acc = 0f;

        foreach (var e in enemies)
        {
            acc += Mathf.Max(0f, e.weight);
            if (r <= acc) return e;
        }
        return enemies[enemies.Count - 1];
    }

    private void Spawn(EnemyEntry entry, Vector2 pos)
    {
        GameObject go = Instantiate(entry.prefab, pos, Quaternion.identity);

        // Track alive count via a simple death callback
        EnemyLife life = go.GetComponent<EnemyLife>();
        if (life != null)
        {
            life.OnDeath += OnEnemyDeath;
        }

        alive++;
        if (entry.isRanged) rangedAlive++;
    }

    private void OnEnemyDeath(EnemyLife life)
    {
        alive--;
        // Determine if it was ranged based on a flag on the enemy (simplest)
        EnemyTag tag = life.GetComponent<EnemyTag>();
        if (tag != null && tag.isRanged) rangedAlive--;

        life.OnDeath -= OnEnemyDeath;
    }
}

