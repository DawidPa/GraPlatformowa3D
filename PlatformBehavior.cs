//Fixing platform lag in progress

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;
    private Rigidbody rb;
    private Vector3 currentPos;
    private Transform playerTransform;
    private bool playerOnPlatform = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }
    void FixedUpdate()
    {
        float t = (Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -0.5f) + 0.5f;
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position, t);
        rb.MovePosition(currentPos);

        if (playerOnPlatform && playerTransform != null)
        {
            playerTransform.position += rb.velocity * Time.fixedDeltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            playerTransform = null;
        }
    }
}
*/