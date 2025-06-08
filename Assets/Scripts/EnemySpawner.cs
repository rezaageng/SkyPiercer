using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyGo;
    public GameObject bossPrefab; // Assign the Boss prefab in the Inspector

    public float maxSpawnRateInSecond = 5f;

    private float spawnEndTime = 30f;
    private float timer = 0f;
    private bool spawningEnded = false;
    private bool bossSpawned = false;

    [HideInInspector]
    public GameObject spawnedBoss = null;

    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSecond);
        InvokeRepeating("IncreaseSpawnRate", 0f, 10f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!spawningEnded && timer >= spawnEndTime)
        {
            spawningEnded = true;
            CancelInvoke("SpawnEnemy");
            CancelInvoke("IncreaseSpawnRate");

            string currentSceneName = SceneManager.GetActiveScene().name;
            if ((currentSceneName == "GamePlay2" || currentSceneName == "GamePlay3") && !bossSpawned)
            {
                SpawnBoss();
            }
        }
    }

    void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            // Set the spawn position to be just above the top-center of the screen
            Vector3 viewportSpawnPoint = new Vector3(0.5f, 1.1f, 0); // y = 1.1 is just off-screen

            float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z);
            viewportSpawnPoint.z = distanceFromCamera;

            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPoint);

            spawnedBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            spawnedBoss.transform.position = new Vector3(spawnedBoss.transform.position.x, spawnedBoss.transform.position.y, 0);

            // If the boss has an EnemyGun script, disable it initially.
            // It will be enabled by BossMovement.cs when it reaches the center.
            EnemyGun gun = spawnedBoss.GetComponent<EnemyGun>();
            if (gun != null)
            {
                gun.enabled = false;
            }

            bossSpawned = true;
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
            maxSpawnRateInSecond--;

        if (maxSpawnRateInSecond <= 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public bool IsSpawningFinished()
    {
        return spawningEnded;
    }

    public bool IsBossSpawned()
    {
        return bossSpawned;
    }
}