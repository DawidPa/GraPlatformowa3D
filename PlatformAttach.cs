using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject Player;
    private Vector3 lastPlatformPosition;
    private bool playerOnPlatform = false;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = Player.transform;
        lastPlatformPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            playerOnPlatform = false;
        }
    }
    //BUG FIX
    private void FixedUpdate()
    {
        Vector3 platformMovement = transform.position - lastPlatformPosition;

        if (playerOnPlatform)
        {
            playerTransform.position += platformMovement;
        }

        lastPlatformPosition = transform.position;
    }
}
