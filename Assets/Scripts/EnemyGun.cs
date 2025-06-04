using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet; // Prefab peluru musuh
    public float fireRate = 2f;    // Jeda waktu antar tembakan

    void Start()
    {
        Invoke("FireEnemyBullet", fireRate); // Mulai tembakan pertama
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGo");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = transform.position;

            // Hitung arah dalam 2D
            Vector2 direction = (playerShip.transform.position - transform.position);
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }

        // Jadwalkan tembakan berikutnya
        Invoke("FireEnemyBullet", fireRate);
    }
}
