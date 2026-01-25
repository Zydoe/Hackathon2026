using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveEnemy
{
    public GameObject prefab;
    public int count = 5;
}

[System.Serializable]
public class WaveConfig
{
    public string waveName = "Wave";
    public List<WaveEnemy> enemies = new List<WaveEnemy>();
    public float spawnInterval = 0.25f;  // time between spawns
}

public class WaveSpawnerFromCaves : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player;
    [SerializeField] private List<CaveSpawnPoint> caves = new List<CaveSpawnPoint>();

    public IEnumerator SpawnWave(WaveConfig wave)
    {
        if (wave == null || caves.Count == 0) yield break;

        foreach (var group in wave.enemies)
        {
            if (group.prefab == null || group.count <= 0) continue;

            for (int i = 0; i < group.count; i++)
            {
                var cave = PickCave();
                Vector2 pos = cave.transform.position;

                GameObject enemy = Instantiate(group.prefab, pos, Quaternion.identity);

                // Make sure enemy is tagged Enemy for cleanup tools, optional
                // enemy.tag = "Enemy"; // only if you want to force it

                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }

    private CaveSpawnPoint PickCave()
    {
        // Try to spawn away from player
        for (int i = 0; i < 8; i++)
        {
            var c = caves[Random.Range(0, caves.Count)];
            if (c != null && c.CanSpawn(player)) return c;
        }
        return caves[Random.Range(0, caves.Count)];
    }
}
