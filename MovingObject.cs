using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float distance = 5f;
    public float speed = 2f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingForward = true;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + direction.normalized * distance;
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    void FixedUpdate()
    {
        Vector3 destination = movingForward ? targetPosition : startPosition;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, destination, speed * Time.fixedDeltaTime);

        if (rb != null)
        {
            rb.MovePosition(newPosition);
        }
        else
        {
            transform.position = newPosition;
        }

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingForward = !movingForward;
        }
    }
}
