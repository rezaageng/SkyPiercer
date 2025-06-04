using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed = 8f;              // Kecepatan peluru
    Vector2 direction;             // Arah peluru
    bool isReady = false;          // Status peluru (siap jalan)



    void Awake()
    {
        // Inisialisasi sudah dilakukan di atas
    }

    // Set arah tembakan dari luar
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;  // Normalisasi agar konsisten
        isReady = true;
    }

    void Update()
    {
        if (isReady)
        {
            // Gerakkan peluru
            Vector2 position = transform.position;
            position += direction * speed * Time.deltaTime;
            transform.position = position;

            // Hancurkan jika keluar dari layar
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if (position.x < min.x || position.x > max.x || position.y < min.y || position.y > max.y)
            {
                Destroy(gameObject);
            }
        }
    }

  void OnTriggerEnter2D(Collider2D col)
{
    if (col.tag == "PlayerShipTag")
    {
        Destroy(gameObject);
    }
}

}
