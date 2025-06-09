using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    readonly float speed = 8f;
    void Update()
    {
        Vector2 position = transform.position;
        position.y += speed * Time.deltaTime;
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyShipTag"))
        {
            Destroy(gameObject);
        }
    }

}
