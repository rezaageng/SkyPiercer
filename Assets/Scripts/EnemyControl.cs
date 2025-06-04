using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed; // Kecepatan gerakan musuh
    public GameObject Explode;

    GameScore gameScore; // Langsung refer ke script, bukan GameObject

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

            // Hancurkan musuh
            Destroy(gameObject);
        }
    }
}
