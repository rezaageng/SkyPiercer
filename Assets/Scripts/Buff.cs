using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType { Health, Bullet }
    public BuffType type;

    public float speed = 1.5f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
}
