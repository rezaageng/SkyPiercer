using UnityEngine;

public class Buff : MonoBehaviour
{
    // Enum to define the different types of buffs available.
    public enum BuffType { Health, Bullet }
    public BuffType type;

    // Speed at which the buff will move down the screen.
    public float speed = 1.5f;

    void Update()
    {
        // Move the buff downwards every frame.
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Destroy the buff if it moves off-screen to save memory.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
}
