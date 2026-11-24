using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 movementDistance = new Vector3(5, 0, 0);
    public float speed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + movementDistance;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            targetPosition = targetPosition == startPosition
                ? startPosition + movementDistance
                : startPosition;
        }
    }
}
