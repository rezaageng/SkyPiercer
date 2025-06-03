using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 min;
    private Vector2 max;
    private Vector2 targetPosition;

    public GameObject PlayerBulletGo;
    public GameObject BulletPosition_1;
    public GameObject BulletPosition_2;

    private bool hasMovedThisFrame = false;

    public float fireRate = 0.2f; // jeda antar tembakan
    private float nextFireTime = 0f;

    void Start()
    {
        // Hitung batas layar
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        min = bottomLeft;
        max = topRight;

        max.x -= 0.225f;
        min.x += 0.225f;

        max.y -= 0.285f;
        min.y += 0.285f;

        targetPosition = transform.position;
    }

    void Update()
    {
        hasMovedThisFrame = false;
        HandleInput();

        // Simpan posisi sebelum bergerak
        Vector2 oldPosition = transform.position;

        // Gerakkan menuju target
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Deteksi apakah benar-benar bergerak
        if ((Vector2)transform.position != oldPosition)
        {
            hasMovedThisFrame = true;
        }

        // Clamp posisi agar tidak keluar layar
        float clampedX = Mathf.Clamp(transform.position.x, min.x, max.x);
        float clampedY = Mathf.Clamp(transform.position.y, min.y, max.y);
        transform.position = new Vector2(clampedX, clampedY);

        // Tembak otomatis jika bergerak & cooldown selesai
        if (hasMovedThisFrame && Time.time >= nextFireTime)
        {
            FireBullets();
            nextFireTime = Time.time + fireRate;
        }

        // Tembakan manual (opsional)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullets();
        }
    }

    void HandleInput()
    {
        // Android (sentuhan)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
            targetPosition = touchPos;
        }

        // PC (mouse klik)
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            targetPosition = mousePos;
        }
    }

    void FireBullets()
    {
        Instantiate(PlayerBulletGo, BulletPosition_1.transform.position, Quaternion.identity);
        Instantiate(PlayerBulletGo, BulletPosition_2.transform.position, Quaternion.identity);
    }
}
