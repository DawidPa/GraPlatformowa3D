//Mouse sensitivity in progress

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityManager : MonoBehaviour
{
    public Slider slider;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100);
        slider.value = mouseSensitivity / 10;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        PlayerPrefs.SetFloat("currentSensitivity", mouseSensitivity);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    public void AdjustSpeed(float newSpeed)
    {
        mouseSensitivity = newSpeed * 10;
    }
}
*/