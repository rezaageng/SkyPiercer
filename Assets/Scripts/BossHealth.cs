using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 150;
    public GameObject explosionPrefab;
    private GameScore gameScore;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    void Start()
    {
        GameObject scoreUIObj = GameObject.FindGameObjectWithTag("ScoreTextTag");
        if (scoreUIObj != null)
        {
            gameScore = scoreUIObj.GetComponent<GameScore>();
        }

        // Ambil SpriteRenderer dan simpan warna aslinya
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Method to be called when the boss is hit
    public void TakeDamage(int amount)
    {
        health -= amount;

        // Efek warna gelap saat terkena tembakan
        if (spriteRenderer != null)
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            flashCoroutine = StartCoroutine(FlashColor());
        }

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

    // Coroutine untuk membuat warna boss menjadi gelap sementara
    System.Collections.IEnumerator FlashColor()
    {
        spriteRenderer.color = new Color(originalColor.r * 0.5f, originalColor.g * 0.5f, originalColor.b * 0.5f);

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = originalColor;
    }
}
