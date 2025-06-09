using System.Collections;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;
    public float fireRate = 2f;
    private Coroutine fireRoutine;

    void OnEnable()
    {
        fireRoutine = StartCoroutine(FireRoutine());
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
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGo");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            Vector2 direction = (playerShip.transform.position - transform.position).normalized;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
