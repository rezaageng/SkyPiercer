using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed; // Kecepatan gerakan musuh
    public GameObject Explode;

    GameScore gameScore;

    // --- NEW VARIABLES FOR BUFF DROPS ---
    // Assign your buff prefabs (HP and Bullet) in the Inspector.
    public GameObject[] buffPrefabs;
    // The chance for a buff to drop, from 0.0 (0%) to 1.0 (100%).
    [Range(0, 1)] public float buffDropChance = 0.1f; // 10% chance

    void Start()
    {
        speed = Random.Range(0.5f, 2.0f);

        GameObject scoreUIObj = GameObject.FindGameObjectWithTag("ScoreTextTag");
        if (scoreUIObj != null)
        {
            gameScore = scoreUIObj.GetComponent<GameScore>();
        }
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.y -= speed * Time.deltaTime;
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerShipTag") || col.CompareTag("PlayerBulletTag"))
        {
            // Tampilkan ledakan
            if (Explode != null)
            {
                Instantiate(Explode, transform.position, Quaternion.identity);
            }

            // Tambah skor
            if (gameScore != null)
            {
                gameScore.Score += 100;
            }

            // --- BUFF DROP LOGIC ---
            // Check if a buff should be dropped based on the drop chance.
            if (Random.value < buffDropChance)
            {
                SpawnRandomBuff();
            }
            // --- END OF BUFF LOGIC ---

            // Hancurkan musuh
            Destroy(gameObject);
        }
    }

    // --- NEW METHOD TO SPAWN A RANDOM BUFF ---
    void SpawnRandomBuff()
    {
        // Ensure there are buff prefabs assigned.
        if (buffPrefabs != null && buffPrefabs.Length > 0)
        {
            // Pick a random buff from the array and spawn it at the enemy's position.
            int randomIndex = Random.Range(0, buffPrefabs.Length);
            Instantiate(buffPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }
}