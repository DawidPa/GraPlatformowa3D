using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1; 
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took damage");

        if (currentHealth <= 0)
        {
            Die();
            PlayerController.instance.Bounce();
        }

    }
    private void Die()
    {
        Debug.Log("Enemy died");

        gameObject.SetActive(false);
     
    }
    public void ResetEnemy()
    {
        currentHealth = maxHealth; 
        gameObject.SetActive(true); 
    }

}
