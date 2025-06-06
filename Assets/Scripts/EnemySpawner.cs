using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyGo;
    public float maxSpawnRateInSecond = 5f;

    private float spawnEndTime = 30f; // Hanya spawn selama 30 detik
    private float timer = 0f;
    private bool spawningEnded = false;

    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSecond);
        InvokeRepeating("IncreaseSpawnRate", 0f, 10f); // Percepat setiap 10 detik
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Hentikan spawn setelah 30 detik
        if (!spawningEnded && timer >= spawnEndTime)
        {
            spawningEnded = true;
            CancelInvoke("SpawnEnemy");
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    void SpawnEnemy()
    {
        if (spawningEnded) return;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject enemy = Instantiate(enemyGo);
        Vector2 spawnPosition = new Vector2(Random.Range(min.x, max.x), max.y);
        enemy.transform.position = spawnPosition;

        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float spawnInSeconds = (maxSpawnRateInSecond > 1f) ? Random.Range(1f, maxSpawnRateInSecond) : 1f;
        Invoke("SpawnEnemy", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSecond > 1f)
        {
            maxSpawnRateInSecond--;
        }
        if (maxSpawnRateInSecond <= 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    public bool IsSpawningFinished()
    {
        return spawningEnded;
    }
}
