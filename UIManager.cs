using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen; 
    public float fadeSpeed = .5f; 

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this; 
    }

    public void ShowBlackScreen()
    {
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 1f);
    }

    public IEnumerator FadeOutBlackScreen()
    {
        float alpha = blackScreen.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeInBlackScreen()
    {
        float alpha = blackScreen.color.a;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            yield return null;
        }
    }
    public void UpdateHealthText(int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }
    public void UpdateCoinText(int coinCount)
    {
        coinText.text = coinCount.ToString();
    }
}