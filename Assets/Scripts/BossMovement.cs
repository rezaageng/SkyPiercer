using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;

    private Vector2 movementDirection;
    private float timer;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float horizontalArea = (max.x - min.x) * 0.6f;
        float centerX = (min.x + max.x) / 2f;

        float verticalTop = max.y - 1f;
        float verticalBottom = max.y - 3f;

        minBounds = new Vector2(centerX - horizontalArea / 2f, verticalBottom);
        maxBounds = new Vector2(centerX + horizontalArea / 2f, verticalTop);

        PickNewDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
        }

        Vector2 newPosition = (Vector2)transform.position + movementDirection * moveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;
    }

    void PickNewDirection()
    {
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f)).normalized;
        timer = 0f;
    }
}
