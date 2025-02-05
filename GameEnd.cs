using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string message;
    public GameObject signPanel;
    public TextMeshProUGUI uiText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!PauseMenu.isPaused && signPanel != null && uiText != null)
            {
                uiText.text = message;
                signPanel.SetActive(true);
                Time.timeScale = 0f; 
                Invoke("QuitGame", 3f); 
            }
        }
    }
    private void QuitGame()
    {
        Debug.Log($"Koniec gry");
        Application.Quit();

    }
}