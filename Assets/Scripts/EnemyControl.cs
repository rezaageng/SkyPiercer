using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // Kecepatan gerakan musuh
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        // Gerakkan musuh ke bawah
        Vector3 position = transform.position;
        position.y -= speed * Time.deltaTime;
        transform.position = position;

        // Hapus musuh jika sudah keluar layar (di bawah)
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
}
