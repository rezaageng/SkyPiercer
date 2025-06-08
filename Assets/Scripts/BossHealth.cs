using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100; // High health for the boss
    public GameObject explosionPrefab;
    private GameScore gameScore;

    void Start()
    {
        // Find the GameScore script in the scene to update the score
        GameObject scoreUIObj = GameObject.FindGameObjectWithTag("ScoreTextTag");
        if (scoreUIObj != null)
        {
            gameScore = scoreUIObj.GetComponent<GameScore>();
        }
    }

    // Method to be called when the boss is hit
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    // What happens when the boss's health reaches zero
    void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (gameScore != null)
        {
            gameScore.Score += 1000; // Award points for defeating the boss
        }

        Destroy(gameObject); // Destroy the boss
    }

    // Detect collision with player bullets
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerBulletTag"))
        {
            TakeDamage(1); // Reduce health by 1 for each bullet hit
            Destroy(col.gameObject); // Destroy the player's bullet
        }
    }
}