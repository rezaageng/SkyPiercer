using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;
    public float fireRate = 2f;

    void Start()
    {
        Invoke("FireEnemyBullet", fireRate);
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGo");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - transform.position;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }

        Invoke("FireEnemyBullet", fireRate);
    }
}
