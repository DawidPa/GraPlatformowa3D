using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject resumeButton;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                CloseOptionMenu();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        EventSystem.current.SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenOptionsMenu()
    {
        optionMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CloseOptionMenu()
    {
        optionMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.ResumeMusic();
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }

    public void SetMusicVolume(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicVolume(value);
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    public void SetSFXVolume(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSFXVolume(value);
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }

}
