using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;
    public GameObject PlayerBulletGo;
    public GameObject BulletPosition_1;
    public GameObject BulletPosition_2;
    public GameObject gameOverImage;
    public GameObject missionCompleteImage;
    public EnemySpawner enemySpawner;
    public float fireRate = 0.2f;
    public GameObject Explode;
    public HealthDisplay healthDisplay;

    private Vector2 min, max, targetPosition;
    private bool hasMovedThisFrame = false;
    private float nextFireTime = 0f;
    private bool missionCompleted = false;
    private bool isPlayerDead = false;
    private float originalFireRate;
    private Coroutine bulletBuffCoroutine;

    void Start()
    {
        originalFireRate = fireRate;

        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        min = new Vector2(bottomLeft.x + 0.225f, bottomLeft.y + 0.285f);
        max = new Vector2(topRight.x - 0.225f, topRight.y - 0.285f);

        targetPosition = transform.position;
    }

    void Update()
    {
        if (isPlayerDead) return;

        hasMovedThisFrame = false;
        HandleInput();

        Vector2 currentPosition = transform.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        transform.position = new Vector2(
            Mathf.Clamp(newPosition.x, min.x, max.x),
            Mathf.Clamp(newPosition.y, min.y, max.y)
        );

        if (newPosition != currentPosition)
        {
            hasMovedThisFrame = true;
        }

        if (hasMovedThisFrame && Time.time >= nextFireTime)
        {
            FireBullets();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullets();
        }
    }

    public void TriggerLevelComplete()
    {
        if (missionCompleted || isPlayerDead) return;

        missionCompleted = true;

        if (missionCompleteImage != null)
            missionCompleteImage.SetActive(true);

        string currentScene = SceneManager.GetActiveScene().name;
        int levelNumber = 0;
        if (int.TryParse(currentScene.Substring("GamePlay".Length), out levelNumber))
        {
            int highestLevelCompleted = PlayerPrefs.GetInt("HighestLevelCompleted", 0);
            if (levelNumber > highestLevelCompleted)
            {
                PlayerPrefs.SetInt("HighestLevelCompleted", levelNumber);
                PlayerPrefs.Save();
                Debug.Log("Game Saved! Highest level completed: " + levelNumber);
            }
        }

        Invoke("LoadNextScene", 3f);
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

        if (col.CompareTag("Buff"))
        {
            Buff buff = col.GetComponent<Buff>();
            if (buff != null)
            {
                ApplyBuff(buff.type);
            }
            Destroy(col.gameObject);
        }
    }

    void ApplyBuff(Buff.BuffType type)
    {
        if (type == Buff.BuffType.Health)
        {
            if (healthDisplay != null && healthDisplay.playerHealth != null)
            {
                healthDisplay.playerHealth.health = Mathf.Min(healthDisplay.playerHealth.health + 1, healthDisplay.playerHealth.maxHealth);
                healthDisplay.SetHealth(healthDisplay.playerHealth.health);
            }
        }
        else if (type == Buff.BuffType.Bullet)
        {
            if (bulletBuffCoroutine != null)
            {
                StopCoroutine(bulletBuffCoroutine);
            }
            bulletBuffCoroutine = StartCoroutine(BulletBuffCoroutine());
        }
    }

    IEnumerator BulletBuffCoroutine()
    {
        fireRate = originalFireRate / 2;
        yield return new WaitForSeconds(10f);
        fireRate = originalFireRate;
        bulletBuffCoroutine = null;
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

    void DestroyPlayer()
    {
        if (isPlayerDead) return;
        isPlayerDead = true;

        if (Explode != null)
            Instantiate(Explode, transform.position, Quaternion.identity);

        if (gameOverImage != null)
            gameOverImage.SetActive(true);

        StartCoroutine(RestartAfterDelay(3f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
