using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 8f; // Kecepatan peluru

    void Update()
    {
        // Gerakkan peluru ke atas menggunakan Vector2
        Vector2 position = transform.position;
        position.y += speed * Time.deltaTime;
        transform.position = position;

        // Dapatkan batas atas layar
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        // Hapus peluru jika melewati batas atas
        if (position.y > max.y)
        {
            Destroy(gameObject);
        }
    }
}
