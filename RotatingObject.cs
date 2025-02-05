using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool rotateAroundX = false;
    public bool rotateAroundY = true;
    public bool rotateAroundZ = false;

    void Update()
    {
        float xRotation = rotateAroundX ? rotationSpeed * Time.deltaTime : 0f;
        float yRotation = rotateAroundY ? rotationSpeed * Time.deltaTime : 0f;
        float zRotation = rotateAroundZ ? rotationSpeed * Time.deltaTime : 0f;
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
