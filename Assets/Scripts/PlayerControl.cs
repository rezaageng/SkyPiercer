using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 min;
    private Vector2 max;
    private Vector2 targetPosition;

    public GameObject PlayerBulletGo;
    public GameObject BulletPosition_1;
    public GameObject BulletPosition_2;

    public GameObject gameOverImage;
    public GameObject missionCompleteImage;

    public EnemySpawner enemySpawner;

    private bool hasMovedThisFrame = false;

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    public GameObject Explode;
    public HealthDisplay healthDisplay;

    private float missionTime = 0f;
    private bool missionCompleted = false;

    private bool isPlayerDead = false;

    void Start()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        min = new Vector2(bottomLeft.x + 0.225f, bottomLeft.y + 0.285f);
        max = new Vector2(topRight.x - 0.225f, topRight.y - 0.285f);

        targetPosition = transform.position;
    }

    void Update()
    {
        if (isPlayerDead) return; // Stop update jika player sudah mati

        hasMovedThisFrame = false;
        HandleInput();

        Vector2 currentPosition = transform.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        transform.position = new Vector2(
            Mathf.Clamp(newPosition.x, min.x, max.x),
            Mathf.Clamp(newPosition.y, min.y, max.y)
        );

        if (newPosition != currentPosition)
            hasMovedThisFrame = true;

        if (hasMovedThisFrame && Time.time >= nextFireTime)
        {
            FireBullets();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullets();
        }

        // Hitung waktu misi
        missionTime += Time.deltaTime;

        if (!missionCompleted && missionTime >= 30f)
        {
            ShowMissionComplete();
        }
    }

    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }

        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void FireBullets()
    {
        Instantiate(PlayerBulletGo, BulletPosition_1.transform.position, Quaternion.identity);
        Instantiate(PlayerBulletGo, BulletPosition_2.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isPlayerDead) return;

        if (col.CompareTag("EnemyShipTag") || col.CompareTag("EnemyBulletTag"))
        {
            if (healthDisplay != null && healthDisplay.playerHealth != null)
            {
                healthDisplay.playerHealth.TakeDamage(1);
                healthDisplay.SetHealth(healthDisplay.playerHealth.health);

                if (healthDisplay.playerHealth.health <= 0)
                {
                    DestroyPlayer();
                }
            }
            else
            {
                DestroyPlayer();
            }

            Destroy(col.gameObject);
        }
    }

    void DestroyPlayer()
    {
        if (isPlayerDead) return;
        isPlayerDead = true;

        if (Explode != null)
            Instantiate(Explode, transform.position, Quaternion.identity);

        if (gameOverImage != null)
            gameOverImage.SetActive(true);

        // Jalankan coroutine untuk restart scene
        StartCoroutine(RestartAfterDelay(3f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowMissionComplete()
    {
        if (!missionCompleted && enemySpawner != null && enemySpawner.IsSpawningFinished())
        {
            if (GameObject.FindGameObjectsWithTag("EnemyShipTag").Length == 0 &&
                healthDisplay != null &&
                healthDisplay.playerHealth.health > 0)
            {
                if (missionCompleteImage != null)
                    missionCompleteImage.SetActive(true);

                missionCompleted = true;

                // Pindah ke scene selanjutnya setelah delay
                Invoke("LoadNextScene", 3f);
            }
        }
    }

    void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GamePlay1")
            SceneManager.LoadScene("GamePlay2");
        else if (currentScene == "GamePlay2")
            SceneManager.LoadScene("GamePlay3");
        else
            SceneManager.LoadScene("MainMenu");
    }
}
