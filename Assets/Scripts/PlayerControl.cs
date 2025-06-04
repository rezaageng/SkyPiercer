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

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    void Start()
    {
        // Hitung batas layar
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        min = new Vector2(bottomLeft.x + 0.225f, bottomLeft.y + 0.285f);
        max = new Vector2(topRight.x - 0.225f, topRight.y - 0.285f);

        targetPosition = transform.position;
    }

    void Update()
    {
        hasMovedThisFrame = false;

        HandleInput();

        Vector2 currentPosition = transform.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        transform.position = newPosition;

        if (newPosition != currentPosition)
        {
            hasMovedThisFrame = true;
        }

        float clampedX = Mathf.Clamp(newPosition.x, min.x, max.x);
        float clampedY = Mathf.Clamp(newPosition.y, min.y, max.y);
        transform.position = new Vector2(clampedX, clampedY);

        if (hasMovedThisFrame && Time.time >= nextFireTime)
        {
            FireBullets();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullets();
        }
    }

    void HandleInput()
    {
        // Android
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            targetPosition = touchPos;
        }

        // PC
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mousePos;
        }
    }

    void FireBullets()
    {
        Instantiate(PlayerBulletGo, BulletPosition_1.transform.position, Quaternion.identity);
        Instantiate(PlayerBulletGo, BulletPosition_2.transform.position, Quaternion.identity);
    }
}
