using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject explosionPrefab;
    private GameScore gameScore;

    void Start()
    {
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

    void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (gameScore != null)
        {
            gameScore.Score += 1000;
        }


        PlayerControl playerControl = FindObjectOfType<PlayerControl>();
        if (playerControl != null)
        {
            playerControl.TriggerLevelComplete();
        }

        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerBulletTag"))
        {
            TakeDamage(1);
            Destroy(col.gameObject);
        }
    }
}