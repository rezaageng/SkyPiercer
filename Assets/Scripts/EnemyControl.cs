using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public GameObject Explode;

    GameScore gameScore;

    public GameObject[] buffPrefabs;

    [Range(0, 1)] public float buffDropChance = 0.1f;

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

            if (Explode != null)
            {
                Instantiate(Explode, transform.position, Quaternion.identity);
            }


            if (gameScore != null)
            {
                gameScore.Score += 100;
            }

            if (Random.value < buffDropChance)
            {
                SpawnRandomBuff();
            }

            Destroy(gameObject);
        }
    }


    void SpawnRandomBuff()
    {

        if (buffPrefabs != null && buffPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, buffPrefabs.Length);
            Instantiate(buffPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }
}