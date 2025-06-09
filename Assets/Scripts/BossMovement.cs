using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 2.5f;
    private Vector3 targetPosition;
    private bool hasReachedCenter = false;

    void Start()
    {
        Vector3 viewportTargetPoint = new Vector3(0.5f, 0.75f, 0);

        float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z);
        viewportTargetPoint.z = distanceFromCamera;

        targetPosition = Camera.main.ViewportToWorldPoint(viewportTargetPoint);
        targetPosition.z = 0;
    }

    void Update()
    {
        if (!hasReachedCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                hasReachedCenter = true;

                EnemyGun gun = GetComponent<EnemyGun>();
                if (gun != null)
                {
                    gun.enabled = true;
                }
            }
        }
    }
}