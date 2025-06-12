using System.Collections;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject EnemyBullet;     // Prefab peluru musuh
    public float fireRate = 1f;        // Waktu jeda antar tembakan

    [Header("Spread Settings")]
    public int numberOfBullets = 3;    // Jumlah peluru dalam satu spread
    public float spreadAngle = 45f;    // Total sudut spread (derajat)

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
        yield return new WaitForSeconds(0.5f); // Delay awal sebelum mulai menembak

        while (true)
        {
            FireEnemyBulletSpread();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void FireEnemyBulletSpread()
    {
        GameObject playerShip = GameObject.Find("PlayerGo");
        if (playerShip == null) return;

        // Arah ke pemain
        Vector2 directionToPlayer = (playerShip.transform.position - transform.position).normalized;

        // Hitung sudut antar peluru
        float angleStep = (numberOfBullets > 1) ? spreadAngle / (numberOfBullets - 1) : 0f;
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + (i * angleStep);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 bulletDirection = rotation * directionToPlayer;

            GameObject bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(bulletDirection);
        }
    }
}
