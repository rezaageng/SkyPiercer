using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 2.5f; // Speed at which the boss moves to the center
    private Vector3 targetPosition;
    private bool hasReachedCenter = false;

    void Start()
    {
        // Set the target position to be near the top-center of the screen.
        // We'll calculate this using viewport coordinates (x=0.5 is center, y=0.75 is 75% from the bottom).
        Vector3 viewportTargetPoint = new Vector3(0.5f, 0.75f, 0);

        // Calculate the world position from the viewport point
        float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z);
        viewportTargetPoint.z = distanceFromCamera;

        targetPosition = Camera.main.ViewportToWorldPoint(viewportTargetPoint);
        targetPosition.z = 0; // Ensure the boss stays on the 2D plane
    }

    void Update()
    {
        // Only move the boss if it hasn't reached the center position yet
        if (!hasReachedCenter)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the boss is close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                hasReachedCenter = true;

                // Once the boss reaches the center, you can enable other behaviors.
                // For example, if you have an EnemyGun script for shooting, you can enable it now.
                EnemyGun gun = GetComponent<EnemyGun>();
                if (gun != null)
                {
                    gun.enabled = true;
                }
            }
        }
    }
}