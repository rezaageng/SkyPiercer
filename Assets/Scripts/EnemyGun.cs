using System.Collections;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;
    public float fireRate = 2f;

    private Coroutine fireRoutine;

    public int numberOfBullets = 5; // Number of bullets in the spread
    public float spreadAngle = 45f; // The angle of the spread


    void OnEnable()
    {

        fireRoutine = StartCoroutine(FireRoutine());


    }

    void OnEnable()
    {
        InvokeRepeating("FireEnemyBullet", 1f, fireRate);

    }

    void OnDisable()
    {

        if (fireRoutine != null)
            StopCoroutine(fireRoutine);
    }

    IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(0.5f); // Delay awal

        while (true)
        {
            FireEnemyBullet();
            yield return new WaitForSeconds(fireRate);
        }

        CancelInvoke("FireEnemyBullet");

    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGo");
        if (playerShip != null)
        {

            GameObject bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            Vector2 direction = (playerShip.transform.position - transform.position).normalized;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);

            Vector2 directionToPlayer = (playerShip.transform.position - transform.position).normalized;
            float angleStep = spreadAngle / (numberOfBullets - 1);
            float startAngle = -spreadAngle / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + (i * angleStep);
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector2 direction = rotation * directionToPlayer;

                GameObject bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
                bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            }

        }
    }
}