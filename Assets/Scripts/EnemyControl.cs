using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed = 1f; // Kecepatan gerakan musuh
    public GameObject Explode;


    void Update()
    {
        // Ambil posisi saat ini (dalam Vector2)
        Vector2 position = transform.position;

        // Gerakkan musuh ke bawah
        position.y -= speed * Time.deltaTime;
        transform.position = position;

        // Hapus musuh jika melewati batas bawah layar
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShipTag" || col.tag == "PlayerBulletTag")
        {
              if (Application.isPlaying)
                    {
                        OnDestroy(); // Panggil OnDestroy untuk efek ledakan
                        Destroy(gameObject);
                    }
                    else
                    {
                        #if UNITY_EDITOR
                                DestroyImmediate(gameObject); // Aman saat Edit Mode
                        #endif
                        }
                                
        }
    }

 void OnDestroy()
    {
        GameObject explode = (GameObject)Instantiate(Explode);
        explode.transform.position = transform.position;
    }

}
