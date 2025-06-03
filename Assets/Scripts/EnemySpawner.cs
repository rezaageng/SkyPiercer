using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyGo;              // Prefab musuh yang akan di-spawn
    public float maxSpawnRateInSecond = 5f; // Waktu awal antar spawn

    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSecond);          // Panggil pertama kali
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);       // Kurangi waktu spawn setiap 30 detik
    }

    void SpawnEnemy()
    {
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        GameObject enemy = Instantiate(enemyGo);
        enemy.transform.position = new Vector3(Random.Range(min.x, max.x), max.y, 0);

        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSecond > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSecond);
        }
        else
        {
            spawnInSeconds = 1f;
        }

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
}
