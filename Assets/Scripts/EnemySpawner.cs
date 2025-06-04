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
        // Ambil batas bawah kiri dan atas kanan layar (x dan y saja)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject enemy = Instantiate(enemyGo);
        
        // Tempatkan musuh secara acak di atas layar (dalam sumbu X)
        Vector2 spawnPosition = new Vector2(Random.Range(min.x, max.x), max.y);
        enemy.transform.position = spawnPosition;

        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float spawnInSeconds = (maxSpawnRateInSecond > 1f)
            ? Random.Range(1f, maxSpawnRateInSecond)
            : 1f;

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
