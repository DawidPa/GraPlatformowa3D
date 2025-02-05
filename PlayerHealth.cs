using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    private PlayerController playerController;
    private Renderer playerRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        playerRenderer = GetComponentInChildren<Renderer>();
        UIManager.instance.UpdateHealthText(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible || !gameObject.activeInHierarchy) return;

        currentHealth -= damage;
        UIManager.instance.UpdateHealthText(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(BecomeInvincible());
        }
    }

    public void TakeHeal(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
            UIManager.instance.UpdateHealthText(currentHealth);
        }
    }

    private void Die()
    {
        GameManager.instance.RespawnPlayer();
        currentHealth = maxHealth;
        UIManager.instance.UpdateHealthText(currentHealth);
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        float elapsedTime = 0f;
        bool isVisible = true;
        while (elapsedTime < invincibilityDuration)
        {
            isVisible = !isVisible;
            playerRenderer.enabled = isVisible;
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.2f;
        }
        playerRenderer.enabled = true;
        isInvincible = false;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isInvincible = false;
        UIManager.instance.UpdateHealthText(currentHealth);
    }
}
