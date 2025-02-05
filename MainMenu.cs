using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 3f;
    public AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(FadeOutBlackScreen()); 
    }
    public void Play()
    {
        audioSource.Stop();  
        StartCoroutine(FadeAndLoadScene());
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
    private IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(FadeInBlackScreen()); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    private IEnumerator FadeInBlackScreen()
    {
        float alpha = blackScreen.color.a;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            yield return null;
        }
    }
    private IEnumerator FadeOutBlackScreen()
    {
        float alpha = blackScreen.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            yield return null;
        }
    }
}
