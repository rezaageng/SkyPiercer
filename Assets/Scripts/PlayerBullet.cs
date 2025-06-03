using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f; // Set the speed of the bullet
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position = new Vector3(position.x, position.y + speed * Time.deltaTime, position.z);

        transform.position = position;

        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        if (transform.position.x > max.x)
        {
            Destroy(gameObject); // Destroy the bullet when it goes off-screen
        }
        
         
        
    }
}
