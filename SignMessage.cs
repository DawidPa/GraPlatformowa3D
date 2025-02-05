using TMPro;
using UnityEngine;

public class SignMessage : MonoBehaviour
{
    public string message;
    public GameObject signPanel;
    public TextMeshProUGUI uiText;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            if (!PauseMenu.isPaused && signPanel != null && uiText != null)
            {
                uiText.text = message;
                signPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (signPanel != null)
            {
                signPanel.SetActive(false);
            }
        }
    }

    private void Update()
    {

        if (PauseMenu.isPaused && signPanel.activeSelf)
        {
            signPanel.SetActive(false);
        }

        if (!PauseMenu.isPaused && isPlayerInRange && !signPanel.activeSelf)
        {
            if (signPanel != null && uiText != null)
            {
                uiText.text = message;
                signPanel.SetActive(true);
            }
        }
    }
}
